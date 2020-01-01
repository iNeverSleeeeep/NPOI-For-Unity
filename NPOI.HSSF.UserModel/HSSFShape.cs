using NPOI.DDF;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.IO;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// An abstract shape.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Serializable]
	public abstract class HSSFShape
	{
		public const int LINEWIDTH_ONE_PT = 12700;

		public const int LINEWIDTH_DEFAULT = 9525;

		public const int LINESTYLE__COLOR_DEFAULT = 134217792;

		public const int FILL__FILLCOLOR_DEFAULT = 134217737;

		public const bool NO_FILL_DEFAULT = true;

		public const int LINESTYLE_SOLID = 0;

		public const int LINESTYLE_DASHSYS = 1;

		public const int LINESTYLE_DOTSYS = 2;

		public const int LINESTYLE_DASHDOTSYS = 3;

		public const int LINESTYLE_DASHDOTDOTSYS = 4;

		public const int LINESTYLE_DOTGEL = 5;

		public const int LINESTYLE_DASHGEL = 6;

		public const int LINESTYLE_LONGDASHGEL = 7;

		public const int LINESTYLE_DASHDOTGEL = 8;

		public const int LINESTYLE_LONGDASHDOTGEL = 9;

		public const int LINESTYLE_LONGDASHDOTDOTGEL = 10;

		public const int LINESTYLE_NONE = -1;

		public const int LINESTYLE_DEFAULT = -1;

		public const int NO_FILLHITTEST_TRUE = 1114112;

		public const int NO_FILLHITTEST_FALSE = 65536;

		private HSSFShape parent;

		[NonSerialized]
		protected HSSFAnchor anchor;

		[NonSerialized]
		protected internal HSSFPatriarch _patriarch;

		private EscherContainerRecord _escherContainer;

		private ObjRecord _objRecord;

		private EscherOptRecord _optRecord;

		private int lineStyleColor = 134217792;

		private int fillColor = 134217737;

		private int lineWidth = 9525;

		private LineStyle lineStyle;

		private bool noFill;

		public virtual int ShapeId
		{
			get
			{
				return ((EscherSpRecord)_escherContainer.GetChildById(-4086)).ShapeId;
			}
			set
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)_escherContainer.GetChildById(-4086);
				escherSpRecord.ShapeId = value;
				CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)_objRecord.SubRecords[0];
				commonObjectDataSubRecord.ObjectId = (short)(value % 1024);
			}
		}

		/// <summary>
		/// Gets the parent shape.
		/// </summary>
		/// <value>The parent.</value>
		public HSSFShape Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		/// <summary>
		/// Gets or sets the anchor that is used by this shape.
		/// </summary>
		/// <value>The anchor.</value>
		public HSSFAnchor Anchor
		{
			get
			{
				return anchor;
			}
			set
			{
				int num = 0;
				int num2 = -1;
				if (parent == null)
				{
					if (value is HSSFChildAnchor)
					{
						throw new ArgumentException("Must use client anchors for shapes directly attached to sheet.");
					}
					EscherClientAnchorRecord escherClientAnchorRecord = (EscherClientAnchorRecord)_escherContainer.GetChildById(-4080);
					if (escherClientAnchorRecord != null)
					{
						for (num = 0; num < _escherContainer.ChildRecords.Count; num++)
						{
							if (_escherContainer.GetChild(num).RecordId == -4080 && num != _escherContainer.ChildRecords.Count - 1)
							{
								num2 = _escherContainer.GetChild(num + 1).RecordId;
							}
						}
						_escherContainer.RemoveChildRecord(escherClientAnchorRecord);
					}
				}
				else
				{
					if (value is HSSFClientAnchor)
					{
						throw new ArgumentException("Must use child anchors for shapes attached to Groups.");
					}
					EscherChildAnchorRecord escherChildAnchorRecord = (EscherChildAnchorRecord)_escherContainer.GetChildById(-4081);
					if (escherChildAnchorRecord != null)
					{
						for (num = 0; num < _escherContainer.ChildRecords.Count; num++)
						{
							if (_escherContainer.GetChild(num).RecordId == -4081 && num != _escherContainer.ChildRecords.Count - 1)
							{
								num2 = _escherContainer.GetChild(num + 1).RecordId;
							}
						}
						_escherContainer.RemoveChildRecord(escherChildAnchorRecord);
					}
				}
				if (-1 == num2)
				{
					_escherContainer.AddChildRecord(value.GetEscherAnchor());
				}
				else
				{
					_escherContainer.AddChildBefore(value.GetEscherAnchor(), num2);
				}
				anchor = value;
			}
		}

		/// <summary>
		/// The color applied to the lines of this shape.
		/// </summary>
		/// <value>The color of the line style.</value>
		public int LineStyleColor
		{
			get
			{
				return ((EscherRGBProperty)_optRecord.Lookup(448))?.RgbColor ?? 134217792;
			}
			set
			{
				SetPropertyValue(new EscherRGBProperty(448, value));
			}
		}

		/// <summary>
		/// Gets or sets the color used to fill this shape.
		/// </summary>
		/// <value>The color of the fill.</value>
		public int FillColor
		{
			get
			{
				return ((EscherRGBProperty)_optRecord.Lookup(385))?.RgbColor ?? 134217737;
			}
			set
			{
				SetPropertyValue(new EscherRGBProperty(385, value));
			}
		}

		/// <summary>
		/// Gets or sets with width of the line in EMUs.  12700 = 1 pt.
		/// </summary>
		/// <value>The width of the line.</value>
		public int LineWidth
		{
			get
			{
				return ((EscherSimpleProperty)_optRecord.Lookup(459))?.PropertyValue ?? 9525;
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(459, value));
			}
		}

		/// <summary>
		/// Gets or sets One of the constants in LINESTYLE_*
		/// </summary>
		/// <value>The line style.</value>
		public LineStyle LineStyle
		{
			get
			{
				return (LineStyle)(((EscherSimpleProperty)_optRecord.Lookup(462))?.PropertyValue ?? (-1));
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(462, (int)value));
				if (LineStyle != 0)
				{
					SetPropertyValue(new EscherSimpleProperty(471, 0));
					if (LineStyle == LineStyle.None)
					{
						SetPropertyValue(new EscherBoolProperty(511, 524288));
					}
					else
					{
						SetPropertyValue(new EscherBoolProperty(511, 524296));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is no fill.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this shape Is not filled with a color; otherwise, <c>false</c>.
		/// </value>
		public bool IsNoFill
		{
			get
			{
				EscherBoolProperty escherBoolProperty = (EscherBoolProperty)_optRecord.Lookup(447);
				if (escherBoolProperty != null)
				{
					return escherBoolProperty.PropertyValue == 1114112;
				}
				return true;
			}
			set
			{
				SetPropertyValue(new EscherBoolProperty(447, value ? 1114112 : 65536));
			}
		}

		/// <summary>
		/// whether this shape is vertically flipped.
		/// </summary>
		public bool IsFlipVertical
		{
			get
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				return (escherSpRecord.Flags & 0x80) != 0;
			}
			set
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				if (value)
				{
					escherSpRecord.Flags |= 128;
				}
				else
				{
					escherSpRecord.Flags &= 2147483519;
				}
			}
		}

		/// <summary>
		/// whether this shape is horizontally flipped.
		/// </summary>
		public bool IsFlipHorizontal
		{
			get
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				return (escherSpRecord.Flags & 0x40) != 0;
			}
			set
			{
				EscherSpRecord escherSpRecord = (EscherSpRecord)GetEscherContainer().GetChildById(-4086);
				if (value)
				{
					escherSpRecord.Flags |= 64;
				}
				else
				{
					escherSpRecord.Flags &= 2147483583;
				}
			}
		}

		/// <summary>
		/// get or set the rotation, in degrees, that is applied to a shape.
		/// Negative values specify rotation in the counterclockwise direction.
		/// Rotation occurs around the center of the shape.
		/// The default value for this property is 0x00000000
		/// </summary>
		public int RotationDegree
		{
			get
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					EscherSimpleProperty escherSimpleProperty = (EscherSimpleProperty)GetOptRecord().Lookup(4);
					if (escherSimpleProperty != null)
					{
						try
						{
							LittleEndian.PutInt(escherSimpleProperty.PropertyValue, memoryStream);
							return LittleEndian.GetShort(memoryStream.ToArray(), 2);
						}
						catch (IOException)
						{
							return 0;
						}
					}
					return 0;
				}
			}
			set
			{
				SetPropertyValue(new EscherSimpleProperty(4, value << 16));
			}
		}

		/// <summary>
		/// Count of all children and their childrens children.
		/// </summary>
		/// <value>The count of all children.</value>
		public virtual int CountOfAllChildren => 1;

		public HSSFPatriarch Patriarch
		{
			get
			{
				return _patriarch;
			}
			set
			{
				_patriarch = value;
			}
		}

		/// creates shapes from existing file
		/// @param spContainer
		/// @param objRecord
		public HSSFShape(EscherContainerRecord spContainer, ObjRecord objRecord)
		{
			_escherContainer = spContainer;
			_objRecord = objRecord;
			_optRecord = (EscherOptRecord)spContainer.GetChildById(-4085);
			anchor = HSSFAnchor.CreateAnchorFromEscher(spContainer);
		}

		/// <summary>
		/// Create a new shape with the specified parent and anchor.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="anchor">The anchor.</param>
		public HSSFShape(HSSFShape parent, HSSFAnchor anchor)
		{
			this.parent = parent;
			this.anchor = anchor;
			_escherContainer = CreateSpContainer();
			_optRecord = (EscherOptRecord)_escherContainer.GetChildById(-4085);
			_objRecord = CreateObjRecord();
		}

		protected abstract EscherContainerRecord CreateSpContainer();

		protected abstract ObjRecord CreateObjRecord();

		internal abstract void AfterRemove(HSSFPatriarch patriarch);

		internal abstract void AfterInsert(HSSFPatriarch patriarch);

		internal EscherContainerRecord GetEscherContainer()
		{
			return _escherContainer;
		}

		/// <summary>
		/// Sets the color applied to the lines of this shape
		/// </summary>
		/// <param name="red">The red.</param>
		/// <param name="green">The green.</param>
		/// <param name="blue">The blue.</param>
		public void SetLineStyleColor(int red, int green, int blue)
		{
			int rgbColor = (blue << 16) | (green << 8) | red;
			SetPropertyValue(new EscherRGBProperty(448, rgbColor));
		}

		protected void SetPropertyValue(EscherProperty property)
		{
			_optRecord.SetEscherProperty(property);
		}

		/// <summary>
		/// Sets the color used to fill this shape.
		/// </summary>
		/// <param name="red">The red.</param>
		/// <param name="green">The green.</param>
		/// <param name="blue">The blue.</param>
		public void SetFillColor(int red, int green, int blue)
		{
			int rgbColor = (blue << 16) | (green << 8) | red;
			SetPropertyValue(new EscherRGBProperty(385, rgbColor));
		}

		internal abstract HSSFShape CloneShape();

		protected internal ObjRecord GetObjRecord()
		{
			return _objRecord;
		}

		protected internal EscherOptRecord GetOptRecord()
		{
			return _optRecord;
		}
	}
}
