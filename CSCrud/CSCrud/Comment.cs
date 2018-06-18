using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSCrud {
    public class Comment : TableEntity {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Karma { get; set; }
        public string Content { get; set; }
    }
}
