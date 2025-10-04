using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.ListItem;
using BU.DTO.DTOs.RequestDTO.Customer;
using DTO.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Agency
{
    public interface IItemListService
    {
        AllListItemDTO GetListItems(string[] parameters);


    }
}
