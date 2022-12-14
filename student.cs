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

      
    public partial class student
    {
        public int RegNo { get; set; }
        [Required (ErrorMessage = "Please enter a name")]
        [MaxLength(20,ErrorMessage ="Max Length Reached") ]
        [MinLength(5, ErrorMessage = "Enter name atleast 5 characters")] 
        [RegularExpression (@"(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",ErrorMessage="Invalid StudentName")]
        public string StudentName { get; set; } 
        [Required (ErrorMessage = "Please select subjectId")]
        public Nullable<int> SubjectId { get; set; }
    
        public virtual subject subject { get; set; }
    }

    
}
