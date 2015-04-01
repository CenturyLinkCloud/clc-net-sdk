using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenturyLinkCloudSDK.ServiceModels
{
    public class GroupHierarchy
    {
        public GroupHierarchy()
        {
            Groups = new List<GroupHierarchy>();
            Servers = new List<ServerState>();
        }

        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public List<GroupHierarchy> Groups { get; set; }

        public List<ServerState> Servers { get; set; }

        public bool Selected { get; set; }
    }
}
