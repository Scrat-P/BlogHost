using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;
using Microsoft.AspNetCore.Identity;

namespace BlogHost.BLL.ServiceInterfaces
{
    public interface ITagService
    {
        IEnumerable<TagDTO> GetPopularTags();
    }
}
