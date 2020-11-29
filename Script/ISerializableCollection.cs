using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface ISerializableCollection 
    {
        /// <summary>
        /// Clears out the collection and resets it to 0 items.
        /// </summary>
        void Clear();

        /// <summary>
        /// Creates a new instances of the corresponding object.
        /// NOTE that this DOES NOT add the item to the collection.
        /// </summary>
        /// <returns>A new object related to this collection, that derives from SerializableObject.</returns>
        SerializableObject Create();


        /// <summary>
        /// Adds a new derived object to this collection.
        /// </summary>
        /// <param name="o">The derived object to add.</param>
        void Add(SerializableObject o);
    }
}
