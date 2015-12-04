using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZU.Semantic.Ontology.Models
{
	public partial class Person : BasePropertyChanged
	{
		#region Properties
		public FullName FullName
		{
			get
			{
				return fullName;
			}
			set
			{
				SetProperty<FullName>(value, ref fullName, "FullName");
			}
		}

		/// <summary>
		/// List of unique? identifiers used to, well, identify a person in the world; this could be email, or phone number, etc. 
		/// </summary>
		public ObservableCollection<Identifier> Ids
		{
			get
			{
				return ids;
			}
			set
			{
				SetProperty<ObservableCollection<Identifier>>(value, ref ids, "Ids");
			}
		}
		#endregion

		#region Fields
		private FullName fullName = new FullName();
		private ObservableCollection<Identifier> ids = new ObservableCollection<Identifier>();
		#endregion
	}

	/// <summary>
	/// This is a really important part of the problem
	/// </summary>
	public partial class FullName : BasePropertyChanged
	{
		#region Properties
		public string DisplayName
		{
			get
			{
				return displayName;
			}
			set
			{
				SetProperty<string>(value, ref displayName, "DisplayName");
			}
		}

		public ObservableCollection<string> GivenNames
		{
			get
			{
				return givenNames;
			}
			set
			{
				SetProperty<ObservableCollection<string>>(value, ref givenNames, "GivenNames");
			}
		}

		#endregion

		#region Fields
		private string displayName = string.Empty;
		private ObservableCollection<string> givenNames = new ObservableCollection<string>();
		#endregion
	}

	/// <summary>
	/// Identifier is actually a tuple; in our case we simply made it reporting changes back to SIM
	/// </summary>
	public partial class Identifier : BasePropertyChanged
	{
		#region Properties
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				SetProperty<string>(value, ref _value, "Value");
			}
		}

		public string IdKind
		{
			get
			{
				return idKind;
			}
			set
			{
				SetProperty<string>(value, ref idKind, "IdKinds");
			}
		}

		#endregion

		#region Fields
		private string _value = string.Empty;
		private string idKind = string.Empty;
		#endregion
	}

	/// <summary>
	/// This is a second approach. We see author as reference to an entity, instead of a standalone thing
	/// In this case, it is a storage of 4 props: DisplayName, Identifier, EntityId, ModelId
	/// this might be a lot of stuff to load/keep in memory
	/// Obviously, we could simply keep a reference to entity/model (two props)
	/// then, we'd have to create special entities for each extracted person
	/// and give user power to link them together (like Android/WP do)
	/// The key, however, is when are we going to match id and actual name... 
	/// 
	/// Once "Author" property is extracted, we do this:
	/// 1. We try to match it to existing entities... if that fails, we create a new entity. 
	/// </summary>
	// class
} // namespace
