﻿using Linchen.Framework.AttributeExtend;
using Linchen.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Libraries.Model
{
    public class User:BaseModel
    {
        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        //public int State { get; set; }

        [Column("state")]
        public int Status { get; set; }

        public int UserType { get; set; }

        public DateTime LastLoginTime { get; set; }

        public int CreatorId { get; set; }//非空没有完成

        public DateTime CreateTime { get; set; }

        public int LastModifierId { get; set; }

        public DateTime LastModifyTime { get; set; }
    }
}
