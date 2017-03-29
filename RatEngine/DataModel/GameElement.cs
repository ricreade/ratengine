using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;
using RatEngine.Engine.Command;

namespace RatEngine.DataModel
{
    /// <summary>
    /// This class is the base class for all game objects populated from the database.
    /// All items included in the database must have ID, Name, and Description properties.
    /// This provides a common set of properties for all game elements to more easily
    /// support commands like "Look".
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class GameElement : IComparable
    {
        /// <summary>
        /// Constructs a new game element.
        /// </summary>
        protected GameElement()
        {
            
        }

        public GameElement(Guid id)
        {
            GameElementID = id;
        }

        /// <summary>
        /// The collection of listeners for this game element.
        /// </summary>
        [DataMember]
        public virtual List<CommandListener> CommandListeners { get; protected set; }

        /// <summary>
        /// The description of this game object.
        /// </summary>
        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// The list of effects describing this game element.
        /// </summary>
        [DataMember]
        public virtual List<Effect> Effects { get; protected set; }

        /// <summary>
        /// The list of flags describing this game element.
        /// </summary>
        [DataMember]
        public virtual List<Flag> Flags { get; protected set; }

        /// <summary>
        /// The unique game identifier for this game object.
        /// </summary>
        [DataMember]
        public virtual Guid GameElementID { get; protected set; }

        /// <summary>
        /// The name of this game object.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
        

        /// <summary>
        /// Adds a command listener to the game element.
        /// </summary>
        /// <param name="listener">The listener to add.  If this value is
        /// null, nothing happens.</param>
        public virtual void AddCommandListener(CommandListener listener)
        {
            if (listener != null)
            {
                InitializeList(CommandListeners);
                CommandListeners.Add(listener);
            }
        }

        /// <summary>
        /// Adds an effect to the game element.
        /// </summary>
        /// <param name="effect">The effect to add.  If this value is null,
        /// nothing happens.</param>
        public virtual void AddEffect(Effect effect)
        {
            if (effect != null)
            {
                InitializeList(Effects);
                Effects.Add(effect);
            }
        }

        /// <summary>
        /// Adds a flag to the game element.
        /// </summary>
        /// <param name="flag">The flag to add.  If this value is null,
        /// nothing happens.</param>
        public virtual void AddFlag(Flag flag)
        {
            if (flag != null)
            {
                InitializeList(Flags);
                Flags.Add(flag);
            }
        }

        public static int Compare(GameElement left, GameElement right)
        {
            if (ReferenceEquals(left, right))
                return 0;

            if (ReferenceEquals(left, null))
                return -1;

            return left.CompareTo(right);
        }

        public virtual int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            GameElement other = obj as GameElement;
            if (other == null)
                throw new ArgumentException("A GameElement is required for comparison.", "obj");

            return ((IComparable)GameElementID).CompareTo(obj);
        }

        public virtual int CompareTo(GameElement other)
        {
            if (ReferenceEquals(other, null))
                return 1;

            return GameElementID.CompareTo(other.GameElementID);
        }

        public override bool Equals(object obj)
        {
            GameElement other = obj as GameElement;
            if (ReferenceEquals(other, null))
                return false;

            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return GameElementID.GetHashCode();
        }

        /// <summary>
        /// Indicates whether the specified effect is present in this game
        /// element.
        /// </summary>
        /// <param name="effect">The effect to locate.</param>
        /// <returns>True if this game element contains the specified effect.
        /// </returns>
        public virtual bool HasEffect(Effect effect)
        {
            InitializeList(Effects);
            return Effects.Find(e => e.CompareTo(effect) == 0) != null;
        }

        /// <summary>
        /// Indicates whether the specified flag is present in this game 
        /// element.
        /// </summary>
        /// <param name="flag">The flag to locate.</param>
        /// <returns>True if this game element contains the specified flag.
        /// </returns>
        public virtual bool HasFlag(Flag flag)
        {
            InitializeList(Flags);
            return Flags.Find(f => f.CompareTo(flag) == 0) != null;
        }

        /// <summary>
        /// Initializes the specified list if it is not already.
        /// </summary>
        /// <typeparam name="T">The type contained in the list.</typeparam>
        /// <param name="list">The list to initialize.</param>
        protected void InitializeList<T>(List<T> list)
        {
            if (list == null)
                list = new List<T>();
        }

        public static bool operator ==(GameElement left, GameElement right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        public static bool operator !=(GameElement left, GameElement right)
        {
            return !(left == right);
        }

        public static bool operator <(GameElement left, GameElement right)
        {
            return (Compare(left, right) < 0);
        }

        public static bool operator >(GameElement left, GameElement right)
        {
            return (Compare(left, right) > 0);
        }

        /// <summary>
        /// Applies the specified command to each of the element's listeners
        /// to respond.  Each listener configured to respond to the specified
        /// command does so.
        /// </summary>
        /// <param name="command">The command to process.</param>
        public virtual void ProcessCommand(GameCommand command)
        {
            if (command != null && command.CommandString != null && command.Source != null)
            {
                foreach (CommandListener listener in CommandListeners)
                {
                    listener.ProcessCommand(command);
                }
            }
        }

        /// <summary>
        /// Removes the specified command listener from the game element if
        /// it exists.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        /// <returns>True if the listener was successfully removed.</returns>
        public virtual bool RemoveCommandListener(CommandListener listener)
        {
            InitializeList(CommandListeners);
            return CommandListeners.Remove(listener);
        }

        /// <summary>
        /// Removes the specified effect from the game element if it exists.
        /// </summary>
        /// <param name="effect">The effect to remove.</param>
        /// <returns>True if the effect was successfully removed.</returns>
        public virtual bool RemoveEffect(Effect effect)
        {
            InitializeList(Effects);
            return Effects.Remove(effect);
        }

        /// <summary>
        /// Removes the specified flag from the game element if it exists.
        /// </summary>
        /// <param name="flag">The flag to remove.</param>
        /// <returns>True if the flag was successfully removed.</returns>
        public virtual bool RemoveFlag(Flag flag)
        {
            InitializeList(Flags);
            return Flags.Remove(flag);
        }
    }
}
