using EscolaPro.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EscolaPro.Core.Model
{
    public class BaseEntity 
    {
        public bool IsActive { get; set; }
        public DateTime CreatedAt => DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDelete { get; set; }
    }
}
