using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob;
using RatEngine.DataSource;

namespace RatEngine.DataModel.World
{
    /// <summary>
    /// Room
    /// Class that represents a Room is the game map.  This class is a major source of coordination
    /// between a number of game objects that interact in game.  PlayerCharacters exist in the game
    /// via Rooms, NPCs encounter the players via Rooms, and Items exist only via Rooms.  The network
    /// of rooms provides the gameplay structure for the MUD.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Room : Inventoried
    {
        protected Room() { }

        /// <summary>
        /// Constructor.  This constructor provides a means to hydrate the Room object from
        /// a database record.  If either the Row or Region are null, this method throws a new
        /// NullReferenceException.
        /// </summary>
        /// <param name="Row">[DataRow] The database record from which to hydrate the Room.</param>
        /// <param name="MapRegion">[Region] The Region for this room.</param>
        public Room(Region region)
        {
            Region = region;
        }
        
        [DataMember]
        public virtual Region Region { get; protected set; }

        [DataMember]
        public virtual List<Creature> Creatures { get; protected set; }

        [DataMember]
        public virtual List<Transition> Transitions { get; protected set; }

        public virtual void AddCreature(Creature creature)
        {
            if (creature != null)
            {
                InitializeList(Creatures);
                Creatures.Add(creature);
            }
        }
        
        public virtual void AddTransition(Transition transition)
        {
            if (transition != null)
            {
                InitializeList(Transitions);
                Transitions.Add(transition);
            }
        }

        public virtual Creature GetCreature(string creatureName)
        {
            InitializeList(Creatures);
            return Creatures.Find(
                creature => creature.Name.ToLower().Equals(creatureName.ToLower()));
        }

        public virtual GameElement GetElement(string elementName)
        {
            GameElement element;
            element = GetCreature(elementName);
            if (!ReferenceEquals(element, null))
                return element;

            element = GetTransition(elementName);
            if (!ReferenceEquals(element, null))
                return element;

            element = GetItem(elementName);
            if (!ReferenceEquals(element, null))
                return element;

            return null;
        }

        public virtual Transition GetTransition(string transitionName)
        {
            InitializeList(Transitions);
            return Transitions.Find(
                transition => transition.Name.ToLower().Equals(transitionName.ToLower()));
        }

        public virtual bool RemoveCreature(Creature creature)
        {
            InitializeList(Creatures);
            return Creatures.Remove(creature);
        }

        public virtual bool RemoveTransition(Transition transition)
        {
            InitializeList(Transitions);
            return Transitions.Remove(transition);
        }
        
    }
}
