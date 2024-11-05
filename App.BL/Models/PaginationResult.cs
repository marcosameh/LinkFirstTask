using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BL.Models
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
