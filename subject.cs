//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    public partial class subject
    {
        public subject()
        {
            this.students = new HashSet<student>();
        }
    
        [Required (ErrorMessage = "Please enter a SubjectId")]
        public int SubjectId { get; set; }
        [Required (ErrorMessage = "Please enter a Subjects")] 
        public string Subjects { get; set; }
    
        public virtual ICollection<student> students { get; set; }
    }
}