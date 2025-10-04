using DAO;
using System;

namespace DA.DAO.DAO.Chat
{
    public class FeChatDAO : IDAO
    {
        public string GetAllQuery => "SELECT * FROM fe_chat WHERE ISNULL(is_deleted, 0) = 0";

        public string GetSingleQuery => "SELECT * FROM fe_chat WHERE chat_id = @ChatId AND ISNULL(is_deleted, 0) = 0";

        public string GridDataQuery => throw new NotImplementedException();

        public string DoArchiveQuery => @"UPDATE fe_chat 
                                          SET is_deleted = 1, updated_by = @UserId, update_date = GETDATE() 
                                          WHERE chat_id = @ChatId";

        public static string GetChatsByUserIdQuery => @"SELECT * FROM fe_chat 
                                                        WHERE (sender_id = @SenderId OR receiver_id = @ReceiverId) 
                                                          AND ISNULL(is_deleted, 0) = 0 
                                                        ORDER BY create_date DESC";

        public static string GetUnreadMessagesQuery => @"SELECT * FROM fe_chat 
                                                         WHERE receiver_id = @UserId AND ISNULL(is_read, 0) = 0 
                                                           AND ISNULL(is_deleted, 0) = 0 
                                                         ORDER BY create_date DESC";

        public string GetAllQyery => throw new NotImplementedException();
        public static string GetChatsBetweenUsersQuery = @"
        SELECT 
    c.chat_id,
    c.sender_id,
    c.receiver_id,
    u.user_name,
    c.message,
    c.message_type,
    c.create_date
FROM 
    fe_chat c
JOIN 
    fe_users u ON c.receiver_id = u.user_id
WHERE 
        (sender_id = @SenderId AND receiver_id = @ReceiverId)
        OR (sender_id = @ReceiverId AND receiver_id = @SenderId)
ORDER BY 
    c.create_date DESC
OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

        public static string GetUserChatList => @"WITH LatestMessages AS (
    SELECT 
        *,
        ROW_NUMBER() OVER (
            PARTITION BY 
                CASE 
                    WHEN sender_id = @UserId THEN receiver_id 
                    ELSE sender_id 
                END
            ORDER BY create_date DESC
        ) AS RowNum
    FROM fe_chat
    WHERE sender_id = @UserId OR receiver_id = @UserId
)
SELECT 
    c.chat_id,
    c.sender_id,
    c.receiver_id,
    c.message,
    c.create_date,
    u1.user_name AS sender_user_name,
    u2.user_name AS receiver_user_name,
    CASE 
        WHEN c.sender_id = @UserId THEN u2.user_name
        ELSE u1.user_name
    END AS Participant
FROM 
    LatestMessages c
JOIN 
    fe_users u1 ON c.sender_id = u1.user_id
JOIN 
    fe_users u2 ON c.receiver_id = u2.user_id
WHERE 
    c.RowNum = 1
ORDER BY 
    c.create_date DESC;";
    }
}
