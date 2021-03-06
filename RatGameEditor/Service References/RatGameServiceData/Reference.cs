﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RatGameEditor.RatGameServiceData {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Realm", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.World", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Realm : RatGameEditor.RatGameServiceData.GameElement {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Region> RegionsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Region> Regions {
            get {
                return this.RegionsField;
            }
            set {
                if ((object.ReferenceEquals(this.RegionsField, value) != true)) {
                    this.RegionsField = value;
                    this.RaisePropertyChanged("Regions");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Flaggable", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Effectable))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.GameElement))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Region))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Inventoried))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Item))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Creature))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Room))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Transition))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Realm))]
    public partial class Flaggable : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Flag> FlagsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Flag> Flags {
            get {
                return this.FlagsField;
            }
            set {
                if ((object.ReferenceEquals(this.FlagsField, value) != true)) {
                    this.FlagsField = value;
                    this.RaisePropertyChanged("Flags");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Effectable", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.GameElement))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Region))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Inventoried))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Item))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Creature))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Room))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Transition))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Realm))]
    public partial class Effectable : RatGameEditor.RatGameServiceData.Flaggable {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Effect> EffectsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Effect> Effects {
            get {
                return this.EffectsField;
            }
            set {
                if ((object.ReferenceEquals(this.EffectsField, value) != true)) {
                    this.EffectsField = value;
                    this.RaisePropertyChanged("Effects");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameElement", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Region))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Inventoried))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Item))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Creature))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Room))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Transition))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Realm))]
    public partial class GameElement : RatGameEditor.RatGameServiceData.Effectable {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid GameIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid GameID {
            get {
                return this.GameIDField;
            }
            set {
                if ((this.GameIDField.Equals(value) != true)) {
                    this.GameIDField = value;
                    this.RaisePropertyChanged("GameID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Region", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.World", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Region : RatGameEditor.RatGameServiceData.GameElement {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.Realm RealmField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Room> RoomsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.Realm Realm {
            get {
                return this.RealmField;
            }
            set {
                if ((object.ReferenceEquals(this.RealmField, value) != true)) {
                    this.RealmField = value;
                    this.RaisePropertyChanged("Realm");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Room> Rooms {
            get {
                return this.RoomsField;
            }
            set {
                if ((object.ReferenceEquals(this.RoomsField, value) != true)) {
                    this.RoomsField = value;
                    this.RaisePropertyChanged("Rooms");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Inventoried", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Inventory", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Item))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Creature))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Room))]
    public partial class Inventoried : RatGameEditor.RatGameServiceData.GameElement {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Item> InventoryField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Item> Inventory {
            get {
                return this.InventoryField;
            }
            set {
                if ((object.ReferenceEquals(this.InventoryField, value) != true)) {
                    this.InventoryField = value;
                    this.RaisePropertyChanged("Inventory");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Item", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Inventory", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Item : RatGameEditor.RatGameServiceData.Inventoried {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsContainerField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsContainer {
            get {
                return this.IsContainerField;
            }
            set {
                if ((this.IsContainerField.Equals(value) != true)) {
                    this.IsContainerField = value;
                    this.RaisePropertyChanged("IsContainer");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Creature", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Mob", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Creature : RatGameEditor.RatGameServiceData.Inventoried {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.Room LocationField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.Room Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Room", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.World", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Room : RatGameEditor.RatGameServiceData.Inventoried {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Creature> CreaturesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.Region RegionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Transition> TransitionsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Creature> Creatures {
            get {
                return this.CreaturesField;
            }
            set {
                if ((object.ReferenceEquals(this.CreaturesField, value) != true)) {
                    this.CreaturesField = value;
                    this.RaisePropertyChanged("Creatures");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.Region Region {
            get {
                return this.RegionField;
            }
            set {
                if ((object.ReferenceEquals(this.RegionField, value) != true)) {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Transition> Transitions {
            get {
                return this.TransitionsField;
            }
            set {
                if ((object.ReferenceEquals(this.TransitionsField, value) != true)) {
                    this.TransitionsField = value;
                    this.RaisePropertyChanged("Transitions");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Transition", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.World", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Transition : RatGameEditor.RatGameServiceData.GameElement {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeywordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.Room RoomFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.Room RoomToField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescriptionFrom {
            get {
                return this.DescriptionFromField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionFromField, value) != true)) {
                    this.DescriptionFromField = value;
                    this.RaisePropertyChanged("DescriptionFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescriptionTo {
            get {
                return this.DescriptionToField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionToField, value) != true)) {
                    this.DescriptionToField = value;
                    this.RaisePropertyChanged("DescriptionTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword {
            get {
                return this.KeywordField;
            }
            set {
                if ((object.ReferenceEquals(this.KeywordField, value) != true)) {
                    this.KeywordField = value;
                    this.RaisePropertyChanged("Keyword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.Room RoomFrom {
            get {
                return this.RoomFromField;
            }
            set {
                if ((object.ReferenceEquals(this.RoomFromField, value) != true)) {
                    this.RoomFromField = value;
                    this.RaisePropertyChanged("RoomFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.Room RoomTo {
            get {
                return this.RoomToField;
            }
            set {
                if ((object.ReferenceEquals(this.RoomToField, value) != true)) {
                    this.RoomToField = value;
                    this.RaisePropertyChanged("RoomTo");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Flag", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Flag : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.FlagTemplate TemplateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.FlagTemplate Template {
            get {
                return this.TemplateField;
            }
            set {
                if ((object.ReferenceEquals(this.TemplateField, value) != true)) {
                    this.TemplateField = value;
                    this.RaisePropertyChanged("Template");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Effect", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Effect : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.EffectTemplate TemplateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.EffectTemplate Template {
            get {
                return this.TemplateField;
            }
            set {
                if ((object.ReferenceEquals(this.TemplateField, value) != true)) {
                    this.TemplateField = value;
                    this.RaisePropertyChanged("Template");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FlagTemplate", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class FlagTemplate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RatGameEditor.RatGameServiceData.FlagDataType DataTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueMaskField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RatGameEditor.RatGameServiceData.FlagDataType DataType {
            get {
                return this.DataTypeField;
            }
            set {
                if ((this.DataTypeField.Equals(value) != true)) {
                    this.DataTypeField = value;
                    this.RaisePropertyChanged("DataType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValueMask {
            get {
                return this.ValueMaskField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueMaskField, value) != true)) {
                    this.ValueMaskField = value;
                    this.RaisePropertyChanged("ValueMask");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FlagDataType", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging")]
    public enum FlagDataType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        String = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Integer = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Decimal = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Boolean = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EffectTemplate", Namespace="http://schemas.datacontract.org/2004/07/RatEngine.DataModel.Tagging", IsReference=true)]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.GameElement))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Realm>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Realm))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Region>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Region))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Room>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Room))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Transition>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Transition))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Inventoried))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Item>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Item))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Effectable))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Flaggable))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Flag>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Flag))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.FlagTemplate))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.FlagDataType))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Effect>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Effect))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<string>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Creature>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RatGameEditor.RatGameServiceData.Creature))]
    public partial class EffectTemplate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object FlagsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Flags {
            get {
                return this.FlagsField;
            }
            set {
                if ((object.ReferenceEquals(this.FlagsField, value) != true)) {
                    this.FlagsField = value;
                    this.RaisePropertyChanged("Flags");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RatGameServiceData.IRatGameService")]
    public interface IRatGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRatGameService/GetRealmList", ReplyAction="http://tempuri.org/IRatGameService/GetRealmListResponse")]
        System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Realm> GetRealmList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRatGameService/GetRealmList", ReplyAction="http://tempuri.org/IRatGameService/GetRealmListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Realm>> GetRealmListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRatGameService/GetRealmNames", ReplyAction="http://tempuri.org/IRatGameService/GetRealmNamesResponse")]
        System.Collections.Generic.List<string> GetRealmNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRatGameService/GetRealmNames", ReplyAction="http://tempuri.org/IRatGameService/GetRealmNamesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetRealmNamesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRatGameServiceChannel : RatGameEditor.RatGameServiceData.IRatGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RatGameServiceClient : System.ServiceModel.ClientBase<RatGameEditor.RatGameServiceData.IRatGameService>, RatGameEditor.RatGameServiceData.IRatGameService {
        
        public RatGameServiceClient() {
        }
        
        public RatGameServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RatGameServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RatGameServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RatGameServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Realm> GetRealmList() {
            return base.Channel.GetRealmList();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<RatGameEditor.RatGameServiceData.Realm>> GetRealmListAsync() {
            return base.Channel.GetRealmListAsync();
        }
        
        public System.Collections.Generic.List<string> GetRealmNames() {
            return base.Channel.GetRealmNames();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetRealmNamesAsync() {
            return base.Channel.GetRealmNamesAsync();
        }
    }
}
