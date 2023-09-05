using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine;

public interface IUnique
{
    /// <summary>
    ///    /// Returns the unique ID of this object.
    ///       /// </summary>
    ///          Guid ID { get; }
    ///          

    int UniqueId { get; set; }
}
