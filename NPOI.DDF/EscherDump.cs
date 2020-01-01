using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// Used to dump the contents of escher records to a PrintStream.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherDump
	{
		private class PropName
		{
			public int id;

			public string name;

			public PropName(int id, string name)
			{
				this.id = id;
				this.name = name;
			}
		}

		/// <summary>
		/// Decodes the escher stream from a byte array and dumps the results to
		/// a print stream.
		/// </summary>
		/// <param name="data">The data array containing the escher records.</param>
		/// <param name="offset">The starting offset within the data array.</param>
		/// <param name="size">The number of bytes to Read.</param>
		public void Dump(byte[] data, int offset, int size)
		{
			IEscherRecordFactory escherRecordFactory = new DefaultEscherRecordFactory();
			int num;
			for (int i = offset; i < offset + size; i += num)
			{
				EscherRecord escherRecord = escherRecordFactory.CreateRecord(data, i);
				num = escherRecord.FillFields(data, i, escherRecordFactory);
				Console.WriteLine(escherRecord.ToString());
			}
		}

		/// <summary>
		/// This version of dump is a translation from the open office escher dump routine.
		/// </summary>
		/// <param name="maxLength">The number of bytes to Read</param>
		/// <param name="in1">An input stream to Read from.</param>
		public void DumpOld(long maxLength, Stream in1)
		{
			long num = maxLength;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			while (!flag && num > 0)
			{
				stringBuilder = new StringBuilder();
				short num2 = LittleEndian.ReadShort(in1);
				short num3 = LittleEndian.ReadShort(in1);
				int num4 = LittleEndian.ReadInt(in1);
				num -= 8;
				string value;
				switch (num3)
				{
				case -4096:
					value = "MsofbtDggContainer";
					break;
				case -4090:
					value = "MsofbtDgg";
					break;
				case -4074:
					value = "MsofbtCLSID";
					break;
				case -4085:
					value = "MsofbtOPT";
					break;
				case -3814:
					value = "MsofbtColorMRU";
					break;
				case -3810:
					value = "MsofbtSplitMenuColors";
					break;
				case -4095:
					value = "MsofbtBstoreContainer";
					break;
				case -4089:
					value = "MsofbtBSE";
					break;
				case -4094:
					value = "MsofbtDgContainer";
					break;
				case -4088:
					value = "MsofbtDg";
					break;
				case -3816:
					value = "MsofbtRegroupItem";
					break;
				case -3808:
					value = "MsofbtColorScheme";
					break;
				case -4093:
					value = "MsofbtSpgrContainer";
					break;
				case -4092:
					value = "MsofbtSpContainer";
					break;
				case -4087:
					value = "MsofbtSpgr";
					break;
				case -4086:
					value = "MsofbtSp";
					break;
				case -4084:
					value = "MsofbtTextbox";
					break;
				case -4083:
					value = "MsofbtClientTextbox";
					break;
				case -4082:
					value = "MsofbtAnchor";
					break;
				case -4081:
					value = "MsofbtChildAnchor";
					break;
				case -4080:
					value = "MsofbtClientAnchor";
					break;
				case -4079:
					value = "MsofbtClientData";
					break;
				case -3809:
					value = "MsofbtOleObject";
					break;
				case -3811:
					value = "MsofbtDeletedPspl";
					break;
				case -4091:
					value = "MsofbtSolverContainer";
					break;
				case -4078:
					value = "MsofbtConnectorRule";
					break;
				case -4077:
					value = "MsofbtAlignRule";
					break;
				case -4076:
					value = "MsofbtArcRule";
					break;
				case -4075:
					value = "MsofbtClientRule";
					break;
				case -4073:
					value = "MsofbtCalloutRule";
					break;
				case -3815:
					value = "MsofbtSelection";
					break;
				case -3806:
					value = "MsofbtUDefProp";
					break;
				default:
					value = ((num3 >= -4072 && num3 <= -3817) ? "MsofbtBLIP" : (((num2 & 0xF) != 15) ? "UNKNOWN ID" : "UNKNOWN container"));
					break;
				}
				stringBuilder.Append("  ");
				stringBuilder.Append(HexDump.ToHex(num3));
				stringBuilder.Append("  ").Append(value).Append(" [");
				stringBuilder.Append(HexDump.ToHex(num2));
				stringBuilder.Append(',');
				stringBuilder.Append(HexDump.ToHex(num4));
				stringBuilder.Append("]  instance: ");
				stringBuilder.Append(HexDump.ToHex((short)(num2 >> 4)));
				Console.WriteLine(stringBuilder.ToString());
				if (num3 == -4089 && 36 <= num && 36 <= num4)
				{
					stringBuilder = new StringBuilder("    btWin32: ");
					byte b = (byte)in1.ReadByte();
					stringBuilder.Append(HexDump.ToHex(b));
					stringBuilder.Append(GetBlipType(b));
					stringBuilder.Append("  btMacOS: ");
					b = (byte)in1.ReadByte();
					stringBuilder.Append(HexDump.ToHex(b));
					stringBuilder.Append(GetBlipType(b));
					Console.WriteLine(stringBuilder.ToString());
					Console.WriteLine("    rgbUid:");
					HexDump.Dump(in1, 0, 16);
					Console.Write("    tag: ");
					OutHex(2, in1);
					Console.WriteLine();
					Console.Write("    size: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    cRef: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    offs: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    usage: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    cbName: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    unused2: ");
					OutHex(4, in1);
					Console.WriteLine();
					Console.Write("    unused3: ");
					OutHex(4, in1);
					Console.WriteLine();
					num -= 36;
					num4 = 0;
				}
				else if (num3 == -4080 && 18 <= num && 18 <= num4)
				{
					Console.Write("    Flag: ");
					OutHex(2, in1);
					Console.WriteLine();
					Console.Write("    Col1: ");
					OutHex(2, in1);
					Console.Write("    dX1: ");
					OutHex(2, in1);
					Console.Write("    Row1: ");
					OutHex(2, in1);
					Console.Write("    dY1: ");
					OutHex(2, in1);
					Console.WriteLine();
					Console.Write("    Col2: ");
					OutHex(2, in1);
					Console.Write("    dX2: ");
					OutHex(2, in1);
					Console.Write("    Row2: ");
					OutHex(2, in1);
					Console.Write("    dY2: ");
					OutHex(2, in1);
					Console.WriteLine();
					num -= 18;
					num4 -= 18;
				}
				else if (num3 == -4085 || num3 == -3806)
				{
					int num5 = 0;
					Console.WriteLine("    PROPID        VALUE");
					while (num4 >= 6 + num5 && num >= 6 + num5)
					{
						short num6 = LittleEndian.ReadShort(in1);
						int num7 = LittleEndian.ReadInt(in1);
						num4 -= 6;
						num -= 6;
						Console.Write("    ");
						Console.Write(HexDump.ToHex(num6));
						Console.Write(" (");
						int num8 = num6 & 0x3FFF;
						Console.Write(" " + num8);
						if ((num6 & -32768) == 0)
						{
							if ((num6 & 0x4000) != 0)
							{
								Console.Write(", fBlipID");
							}
							Console.Write(")  ");
							Console.Write(HexDump.ToHex(num7));
							if ((num6 & 0x4000) == 0)
							{
								Console.Write(" (");
								Console.Write(Dec1616(num7));
								Console.Write(')');
								Console.Write(" {" + PropertyName((short)num8) + "}");
							}
							Console.WriteLine();
						}
						else
						{
							Console.Write(", fComplex)  ");
							Console.Write(HexDump.ToHex(num7));
							Console.Write(" - Complex prop len");
							Console.WriteLine(" {" + PropertyName((short)num8) + "}");
							num5 += num7;
						}
					}
					while ((num5 & num) > 0)
					{
						short num9 = (num5 > (int)num) ? ((short)num) : ((short)num5);
						HexDump.Dump(in1, 0, num9);
						num5 -= num9;
						num4 -= num9;
						num -= num9;
					}
				}
				else if (num3 == -4078)
				{
					Console.Write("    Connector rule: ");
					Console.Write(LittleEndian.ReadInt(in1));
					Console.Write("    ShapeID A: ");
					Console.Write(LittleEndian.ReadInt(in1));
					Console.Write("   ShapeID B: ");
					Console.Write(LittleEndian.ReadInt(in1));
					Console.Write("    ShapeID connector: ");
					Console.Write(LittleEndian.ReadInt(in1));
					Console.Write("   Connect pt A: ");
					Console.Write(LittleEndian.ReadInt(in1));
					Console.Write("   Connect pt B: ");
					Console.WriteLine(LittleEndian.ReadInt(in1));
					num4 -= 24;
					num -= 24;
				}
				else if (num3 >= -4072 && num3 < -3817)
				{
					Console.WriteLine("    Secondary UID: ");
					HexDump.Dump(in1, 0, 16);
					Console.WriteLine("    Cache of size: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Boundary top: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Boundary left: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Boundary width: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Boundary height: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    X: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Y: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Cache of saved size: " + HexDump.ToHex(LittleEndian.ReadInt(in1)));
					Console.WriteLine("    Compression Flag: " + HexDump.ToHex((byte)in1.ReadByte()));
					Console.WriteLine("    Filter: " + HexDump.ToHex((byte)in1.ReadByte()));
					Console.WriteLine("    Data (after decompression): ");
					num4 -= 50;
					num -= 50;
					short num9 = (num4 > (int)num) ? ((short)num) : ((short)num4);
					byte[] array = new byte[num9];
					for (int i = in1.Read(array, 0, array.Length); i != -1 && i < num9; i += in1.Read(array, i, array.Length))
					{
					}
					using (MemoryStream baseInputStream = new MemoryStream(array))
					{
						Inflater inf = new Inflater(noHeader: false);
						using (InflaterInputStream inStream = new InflaterInputStream(baseInputStream, inf))
						{
							int bytesToDump = -1;
							HexDump.Dump(inStream, 0, bytesToDump);
							num4 -= num9;
							num -= num9;
						}
					}
				}
				if ((num2 & 0xF) == 15 && num >= 0)
				{
					if (num4 <= (int)num)
					{
						Console.WriteLine("            completed within");
					}
					else
					{
						Console.WriteLine("            continued elsewhere");
					}
				}
				else if (num >= 0)
				{
					short num9 = (num4 > (int)num) ? ((short)num) : ((short)num4);
					if (num9 != 0)
					{
						HexDump.Dump(in1, 0, num9);
						num -= num9;
					}
				}
				else
				{
					Console.WriteLine(" >> OVERRUN <<");
				}
			}
		}

		/// <summary>
		/// Returns a property name given a property id.  This is used only by the
		/// old escher dump routine.
		/// </summary>
		/// <param name="propertyId">The property number for the name</param>
		/// <returns>A descriptive name.</returns>
		private string PropertyName(short propertyId)
		{
			PropName[] array = new PropName[274]
			{
				new PropName(4, "transform.rotation"),
				new PropName(119, "protection.lockrotation"),
				new PropName(120, "protection.lockaspectratio"),
				new PropName(121, "protection.lockposition"),
				new PropName(122, "protection.lockagainstselect"),
				new PropName(123, "protection.lockcropping"),
				new PropName(124, "protection.lockvertices"),
				new PropName(125, "protection.locktext"),
				new PropName(126, "protection.lockadjusthandles"),
				new PropName(127, "protection.lockagainstgrouping"),
				new PropName(128, "text.textid"),
				new PropName(129, "text.textleft"),
				new PropName(130, "text.texttop"),
				new PropName(131, "text.textright"),
				new PropName(132, "text.textbottom"),
				new PropName(133, "text.wraptext"),
				new PropName(134, "text.scaletext"),
				new PropName(135, "text.anchortext"),
				new PropName(136, "text.textflow"),
				new PropName(137, "text.fontrotation"),
				new PropName(138, "text.idofnextshape"),
				new PropName(139, "text.bidir"),
				new PropName(187, "text.singleclickselects"),
				new PropName(188, "text.usehostmargins"),
				new PropName(189, "text.rotatetextwithshape"),
				new PropName(190, "text.sizeshapetofittext"),
				new PropName(191, "text.sizetexttofitshape"),
				new PropName(192, "geotext.unicode"),
				new PropName(193, "geotext.rtftext"),
				new PropName(194, "geotext.alignmentoncurve"),
				new PropName(195, "geotext.defaultpointsize"),
				new PropName(196, "geotext.textspacing"),
				new PropName(197, "geotext.fontfamilyname"),
				new PropName(240, "geotext.reverseroworder"),
				new PropName(241, "geotext.hastexteffect"),
				new PropName(242, "geotext.rotatecharacters"),
				new PropName(243, "geotext.kerncharacters"),
				new PropName(244, "geotext.tightortrack"),
				new PropName(245, "geotext.stretchtofitshape"),
				new PropName(246, "geotext.charboundingbox"),
				new PropName(247, "geotext.scaletextonpath"),
				new PropName(248, "geotext.stretchcharheight"),
				new PropName(249, "geotext.nomeasurealongpath"),
				new PropName(250, "geotext.boldfont"),
				new PropName(251, "geotext.italicfont"),
				new PropName(252, "geotext.underlinefont"),
				new PropName(253, "geotext.shadowfont"),
				new PropName(254, "geotext.smallcapsfont"),
				new PropName(255, "geotext.strikethroughfont"),
				new PropName(256, "blip.cropfromtop"),
				new PropName(257, "blip.cropfrombottom"),
				new PropName(258, "blip.cropfromleft"),
				new PropName(259, "blip.cropfromright"),
				new PropName(260, "blip.bliptodisplay"),
				new PropName(261, "blip.blipfilename"),
				new PropName(262, "blip.blipflags"),
				new PropName(263, "blip.transparentcolor"),
				new PropName(264, "blip.contrastSetting"),
				new PropName(265, "blip.brightnessSetting"),
				new PropName(266, "blip.gamma"),
				new PropName(267, "blip.pictureid"),
				new PropName(268, "blip.doublemod"),
				new PropName(269, "blip.picturefillmod"),
				new PropName(270, "blip.pictureline"),
				new PropName(271, "blip.printblip"),
				new PropName(272, "blip.printblipfilename"),
				new PropName(273, "blip.printflags"),
				new PropName(316, "blip.nohittestpicture"),
				new PropName(317, "blip.picturegray"),
				new PropName(318, "blip.picturebilevel"),
				new PropName(319, "blip.pictureactive"),
				new PropName(320, "geometry.left"),
				new PropName(321, "geometry.top"),
				new PropName(322, "geometry.right"),
				new PropName(323, "geometry.bottom"),
				new PropName(324, "geometry.shapepath"),
				new PropName(325, "geometry.vertices"),
				new PropName(326, "geometry.segmentinfo"),
				new PropName(327, "geometry.adjustvalue"),
				new PropName(328, "geometry.adjust2value"),
				new PropName(329, "geometry.adjust3value"),
				new PropName(330, "geometry.adjust4value"),
				new PropName(331, "geometry.adjust5value"),
				new PropName(332, "geometry.adjust6value"),
				new PropName(333, "geometry.adjust7value"),
				new PropName(334, "geometry.adjust8value"),
				new PropName(335, "geometry.adjust9value"),
				new PropName(336, "geometry.adjust10value"),
				new PropName(378, "geometry.shadowOK"),
				new PropName(379, "geometry.3dok"),
				new PropName(380, "geometry.lineok"),
				new PropName(381, "geometry.geotextok"),
				new PropName(382, "geometry.fillshadeshapeok"),
				new PropName(383, "geometry.fillok"),
				new PropName(384, "fill.filltype"),
				new PropName(385, "fill.fillcolor"),
				new PropName(386, "fill.fillopacity"),
				new PropName(387, "fill.fillbackcolor"),
				new PropName(388, "fill.backopacity"),
				new PropName(389, "fill.crmod"),
				new PropName(390, "fill.patterntexture"),
				new PropName(391, "fill.blipfilename"),
				new PropName(392, "fill.blipflags"),
				new PropName(393, "fill.width"),
				new PropName(394, "fill.height"),
				new PropName(395, "fill.angle"),
				new PropName(396, "fill.focus"),
				new PropName(397, "fill.toleft"),
				new PropName(398, "fill.totop"),
				new PropName(399, "fill.toright"),
				new PropName(400, "fill.tobottom"),
				new PropName(401, "fill.rectleft"),
				new PropName(402, "fill.recttop"),
				new PropName(403, "fill.rectright"),
				new PropName(404, "fill.rectbottom"),
				new PropName(405, "fill.dztype"),
				new PropName(406, "fill.shadepReset"),
				new PropName(407, "fill.shadecolors"),
				new PropName(408, "fill.originx"),
				new PropName(409, "fill.originy"),
				new PropName(410, "fill.shapeoriginx"),
				new PropName(411, "fill.shapeoriginy"),
				new PropName(412, "fill.shadetype"),
				new PropName(443, "fill.filled"),
				new PropName(444, "fill.hittestfill"),
				new PropName(445, "fill.shape"),
				new PropName(446, "fill.userect"),
				new PropName(447, "fill.nofillhittest"),
				new PropName(448, "linestyle.color"),
				new PropName(449, "linestyle.opacity"),
				new PropName(450, "linestyle.backcolor"),
				new PropName(451, "linestyle.crmod"),
				new PropName(452, "linestyle.linetype"),
				new PropName(453, "linestyle.fillblip"),
				new PropName(454, "linestyle.fillblipname"),
				new PropName(455, "linestyle.fillblipflags"),
				new PropName(456, "linestyle.fillwidth"),
				new PropName(457, "linestyle.fillheight"),
				new PropName(458, "linestyle.filldztype"),
				new PropName(459, "linestyle.linewidth"),
				new PropName(460, "linestyle.linemiterlimit"),
				new PropName(461, "linestyle.linestyle"),
				new PropName(462, "linestyle.linedashing"),
				new PropName(463, "linestyle.linedashstyle"),
				new PropName(464, "linestyle.linestartarrowhead"),
				new PropName(465, "linestyle.lineendarrowhead"),
				new PropName(466, "linestyle.linestartarrowwidth"),
				new PropName(467, "linestyle.lineestartarrowLength"),
				new PropName(468, "linestyle.lineendarrowwidth"),
				new PropName(469, "linestyle.lineendarrowLength"),
				new PropName(470, "linestyle.linejoinstyle"),
				new PropName(471, "linestyle.lineendcapstyle"),
				new PropName(507, "linestyle.arrowheadsok"),
				new PropName(508, "linestyle.anyline"),
				new PropName(509, "linestyle.hitlinetest"),
				new PropName(510, "linestyle.linefillshape"),
				new PropName(511, "linestyle.nolinedrawdash"),
				new PropName(512, "shadowstyle.type"),
				new PropName(513, "shadowstyle.color"),
				new PropName(514, "shadowstyle.highlight"),
				new PropName(515, "shadowstyle.crmod"),
				new PropName(516, "shadowstyle.opacity"),
				new PropName(517, "shadowstyle.offsetx"),
				new PropName(518, "shadowstyle.offsety"),
				new PropName(519, "shadowstyle.secondoffsetx"),
				new PropName(520, "shadowstyle.secondoffsety"),
				new PropName(521, "shadowstyle.scalextox"),
				new PropName(522, "shadowstyle.scaleytox"),
				new PropName(523, "shadowstyle.scalextoy"),
				new PropName(524, "shadowstyle.scaleytoy"),
				new PropName(525, "shadowstyle.perspectivex"),
				new PropName(526, "shadowstyle.perspectivey"),
				new PropName(527, "shadowstyle.weight"),
				new PropName(528, "shadowstyle.originx"),
				new PropName(529, "shadowstyle.originy"),
				new PropName(574, "shadowstyle.shadow"),
				new PropName(575, "shadowstyle.shadowobsured"),
				new PropName(576, "perspective.type"),
				new PropName(577, "perspective.offsetx"),
				new PropName(578, "perspective.offsety"),
				new PropName(579, "perspective.scalextox"),
				new PropName(580, "perspective.scaleytox"),
				new PropName(581, "perspective.scalextoy"),
				new PropName(582, "perspective.scaleytox"),
				new PropName(583, "perspective.perspectivex"),
				new PropName(584, "perspective.perspectivey"),
				new PropName(585, "perspective.weight"),
				new PropName(586, "perspective.originx"),
				new PropName(587, "perspective.originy"),
				new PropName(639, "perspective.perspectiveon"),
				new PropName(640, "3d.specularamount"),
				new PropName(661, "3d.diffuseamount"),
				new PropName(662, "3d.shininess"),
				new PropName(663, "3d.edGethickness"),
				new PropName(664, "3d.extrudeforward"),
				new PropName(665, "3d.extrudebackward"),
				new PropName(666, "3d.extrudeplane"),
				new PropName(667, "3d.extrusioncolor"),
				new PropName(648, "3d.crmod"),
				new PropName(700, "3d.3deffect"),
				new PropName(701, "3d.metallic"),
				new PropName(702, "3d.useextrusioncolor"),
				new PropName(703, "3d.lightface"),
				new PropName(704, "3dstyle.yrotationangle"),
				new PropName(705, "3dstyle.xrotationangle"),
				new PropName(706, "3dstyle.rotationaxisx"),
				new PropName(707, "3dstyle.rotationaxisy"),
				new PropName(708, "3dstyle.rotationaxisz"),
				new PropName(709, "3dstyle.rotationangle"),
				new PropName(710, "3dstyle.rotationcenterx"),
				new PropName(711, "3dstyle.rotationcentery"),
				new PropName(712, "3dstyle.rotationcenterz"),
				new PropName(713, "3dstyle.rendermode"),
				new PropName(714, "3dstyle.tolerance"),
				new PropName(715, "3dstyle.xviewpoint"),
				new PropName(716, "3dstyle.yviewpoint"),
				new PropName(717, "3dstyle.zviewpoint"),
				new PropName(718, "3dstyle.originx"),
				new PropName(719, "3dstyle.originy"),
				new PropName(720, "3dstyle.skewangle"),
				new PropName(721, "3dstyle.skewamount"),
				new PropName(722, "3dstyle.ambientintensity"),
				new PropName(723, "3dstyle.keyx"),
				new PropName(724, "3dstyle.keyy"),
				new PropName(725, "3dstyle.keyz"),
				new PropName(726, "3dstyle.keyintensity"),
				new PropName(727, "3dstyle.fillx"),
				new PropName(728, "3dstyle.filly"),
				new PropName(729, "3dstyle.fillz"),
				new PropName(730, "3dstyle.fillintensity"),
				new PropName(763, "3dstyle.constrainrotation"),
				new PropName(764, "3dstyle.rotationcenterauto"),
				new PropName(765, "3dstyle.parallel"),
				new PropName(766, "3dstyle.keyharsh"),
				new PropName(767, "3dstyle.fillharsh"),
				new PropName(769, "shape.master"),
				new PropName(771, "shape.connectorstyle"),
				new PropName(772, "shape.blackandwhiteSettings"),
				new PropName(773, "shape.wmodepurebw"),
				new PropName(774, "shape.wmodebw"),
				new PropName(826, "shape.oleicon"),
				new PropName(827, "shape.preferrelativeresize"),
				new PropName(828, "shape.lockshapetype"),
				new PropName(830, "shape.deleteattachedobject"),
				new PropName(831, "shape.backgroundshape"),
				new PropName(832, "callout.callouttype"),
				new PropName(833, "callout.xycalloutgap"),
				new PropName(834, "callout.calloutangle"),
				new PropName(835, "callout.calloutdroptype"),
				new PropName(836, "callout.calloutdropspecified"),
				new PropName(837, "callout.calloutLengthspecified"),
				new PropName(889, "callout.iscallout"),
				new PropName(890, "callout.calloutaccentbar"),
				new PropName(891, "callout.callouttextborder"),
				new PropName(892, "callout.calloutminusx"),
				new PropName(893, "callout.calloutminusy"),
				new PropName(894, "callout.dropauto"),
				new PropName(895, "callout.Lengthspecified"),
				new PropName(896, "groupshape.shapename"),
				new PropName(897, "groupshape.description"),
				new PropName(898, "groupshape.hyperlink"),
				new PropName(899, "groupshape.wrappolygonvertices"),
				new PropName(900, "groupshape.wrapdistleft"),
				new PropName(901, "groupshape.wrapdisttop"),
				new PropName(902, "groupshape.wrapdistright"),
				new PropName(903, "groupshape.wrapdistbottom"),
				new PropName(904, "groupshape.regroupid"),
				new PropName(953, "groupshape.editedwrap"),
				new PropName(954, "groupshape.behinddocument"),
				new PropName(955, "groupshape.ondblclicknotify"),
				new PropName(956, "groupshape.isbutton"),
				new PropName(957, "groupshape.1dadjustment"),
				new PropName(958, "groupshape.hidden"),
				new PropName(959, "groupshape.print")
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].id == propertyId)
				{
					return array[i].name;
				}
			}
			return "unknown property";
		}

		/// <summary>
		/// Returns the blip description given a blip id.
		/// </summary>
		/// <param name="b">blip id</param>
		/// <returns> A description.</returns>
		private string GetBlipType(byte b)
		{
			switch (b)
			{
			case 0:
				return " ERROR";
			case 1:
				return " UNKNOWN";
			case 2:
				return " EMF";
			case 3:
				return " WMF";
			case 4:
				return " PICT";
			case 5:
				return " JPEG";
			case 6:
				return " PNG";
			case 7:
				return " DIB";
			default:
				if (b < 32)
				{
					return " NotKnown";
				}
				return " Client";
			}
		}

		/// <summary>
		/// Straight conversion from OO.  Converts a type of float.
		/// </summary>
		/// <param name="n32">The N32.</param>
		/// <returns></returns>
		private string Dec1616(int n32)
		{
			string arg = "";
			arg += (short)(n32 >> 16);
			arg += '.';
			return arg + (short)(n32 & -1);
		}

		/// <summary>
		/// Dumps out a hex value by Reading from a input stream.
		/// </summary>
		/// <param name="bytes">How many bytes this hex value consists of.</param>
		/// <param name="in1">The stream to Read the hex value from.</param>
		private void OutHex(int bytes, Stream in1)
		{
			switch (bytes)
			{
			case 1:
				Console.Write(HexDump.ToHex((byte)in1.ReadByte()));
				break;
			case 2:
				Console.Write(HexDump.ToHex(LittleEndian.ReadShort(in1)));
				break;
			case 4:
				Console.Write(HexDump.ToHex(LittleEndian.ReadInt(in1)));
				break;
			default:
				throw new IOException("Unable to output variable of that width");
			}
		}

		/// <summary>
		/// Dumps the specified record size.
		/// </summary>
		/// <param name="recordSize">Size of the record.</param>
		/// <param name="data">The data.</param>
		public void Dump(int recordSize, byte[] data)
		{
			Dump(data, 0, recordSize);
		}
	}
}
