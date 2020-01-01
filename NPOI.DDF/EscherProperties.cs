using System.Collections;

namespace NPOI.DDF
{
	/// <summary>
	/// Provides a list of all known escher properties including the description and
	/// type.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherProperties
	{
		public const short TRANSFORM__ROTATION = 4;

		public const short PROTECTION__LOCKROTATION = 119;

		public const short PROTECTION__LOCKASPECTRATIO = 120;

		public const short PROTECTION__LOCKPOSITION = 121;

		public const short PROTECTION__LOCKAGAINSTSELECT = 122;

		public const short PROTECTION__LOCKCROPPING = 123;

		public const short PROTECTION__LOCKVERTICES = 124;

		public const short PROTECTION__LOCKTEXT = 125;

		public const short PROTECTION__LOCKADJUSTHANDLES = 126;

		public const short PROTECTION__LOCKAGAINSTGROUPING = 127;

		public const short TEXT__TEXTID = 128;

		public const short TEXT__TEXTLEFT = 129;

		public const short TEXT__TEXTTOP = 130;

		public const short TEXT__TEXTRIGHT = 131;

		public const short TEXT__TEXTBOTTOM = 132;

		public const short TEXT__WRAPTEXT = 133;

		public const short TEXT__SCALETEXT = 134;

		public const short TEXT__ANCHORTEXT = 135;

		public const short TEXT__TEXTFLOW = 136;

		public const short TEXT__FONTROTATION = 137;

		public const short TEXT__IDOFNEXTSHAPE = 138;

		public const short TEXT__BIDIR = 139;

		public const short TEXT__SINGLECLICKSELECTS = 187;

		public const short TEXT__USEHOSTMARGINS = 188;

		public const short TEXT__ROTATETEXTWITHSHAPE = 189;

		public const short TEXT__SIZESHAPETOFITTEXT = 190;

		public const short TEXT__SIZE_TEXT_TO_FIT_SHAPE = 191;

		public const short GEOTEXT__UNICODE = 192;

		public const short GEOTEXT__RTFTEXT = 193;

		public const short GEOTEXT__ALIGNMENTONCURVE = 194;

		public const short GEOTEXT__DEFAULTPOINTSIZE = 195;

		public const short GEOTEXT__TEXTSPACING = 196;

		public const short GEOTEXT__FONTFAMILYNAME = 197;

		public const short GEOTEXT__REVERSEROWORDER = 240;

		public const short GEOTEXT__HASTEXTEFFECT = 241;

		public const short GEOTEXT__ROTATECHARACTERS = 242;

		public const short GEOTEXT__KERNCHARACTERS = 243;

		public const short GEOTEXT__TIGHTORTRACK = 244;

		public const short GEOTEXT__STRETCHTOFITSHAPE = 245;

		public const short GEOTEXT__CHARBOUNDINGBOX = 246;

		public const short GEOTEXT__SCALETEXTONPATH = 247;

		public const short GEOTEXT__STRETCHCHARHEIGHT = 248;

		public const short GEOTEXT__NOMEASUREALONGPATH = 249;

		public const short GEOTEXT__BOLDFONT = 250;

		public const short GEOTEXT__ITALICFONT = 251;

		public const short GEOTEXT__UNDERLINEFONT = 252;

		public const short GEOTEXT__SHADOWFONT = 253;

		public const short GEOTEXT__SMALLCAPSFONT = 254;

		public const short GEOTEXT__STRIKETHROUGHFONT = 255;

		public const short BLIP__CROPFROMTOP = 256;

		public const short BLIP__CROPFROMBOTTOM = 257;

		public const short BLIP__CROPFROMLEFT = 258;

		public const short BLIP__CROPFROMRIGHT = 259;

		public const short BLIP__BLIPTODISPLAY = 260;

		public const short BLIP__BLIPFILENAME = 261;

		public const short BLIP__BLIPFLAGS = 262;

		public const short BLIP__TRANSPARENTCOLOR = 263;

		public const short BLIP__CONTRASTSetTING = 264;

		public const short BLIP__BRIGHTNESSSetTING = 265;

		public const short BLIP__GAMMA = 266;

		public const short BLIP__PICTUREID = 267;

		public const short BLIP__DOUBLEMOD = 268;

		public const short BLIP__PICTUREFillMOD = 269;

		public const short BLIP__PICTURELINE = 270;

		public const short BLIP__PRINTBLIP = 271;

		public const short BLIP__PRINTBLIPFILENAME = 272;

		public const short BLIP__PRINTFLAGS = 273;

		public const short BLIP__NOHITTESTPICTURE = 316;

		public const short BLIP__PICTUREGRAY = 317;

		public const short BLIP__PICTUREBILEVEL = 318;

		public const short BLIP__PICTUREACTIVE = 319;

		public const short GEOMETRY__LEFT = 320;

		public const short GEOMETRY__TOP = 321;

		public const short GEOMETRY__RIGHT = 322;

		public const short GEOMETRY__BOTTOM = 323;

		public const short GEOMETRY__SHAPEPATH = 324;

		public const short GEOMETRY__VERTICES = 325;

		public const short GEOMETRY__SEGMENTINFO = 326;

		public const short GEOMETRY__ADJUSTVALUE = 327;

		public const short GEOMETRY__ADJUST2VALUE = 328;

		public const short GEOMETRY__ADJUST3VALUE = 329;

		public const short GEOMETRY__ADJUST4VALUE = 330;

		public const short GEOMETRY__ADJUST5VALUE = 331;

		public const short GEOMETRY__ADJUST6VALUE = 332;

		public const short GEOMETRY__ADJUST7VALUE = 333;

		public const short GEOMETRY__ADJUST8VALUE = 334;

		public const short GEOMETRY__ADJUST9VALUE = 335;

		public const short GEOMETRY__ADJUST10VALUE = 336;

		public const short GEOMETRY__SHADOWok = 378;

		public const short GEOMETRY__3DOK = 379;

		public const short GEOMETRY__LINEOK = 380;

		public const short GEOMETRY__GEOTEXTOK = 381;

		public const short GEOMETRY__FILLSHADESHAPEOK = 382;

		public const short GEOMETRY__FILLOK = 383;

		public const short FILL__FILLTYPE = 384;

		public const short FILL__FILLCOLOR = 385;

		public const short FILL__FILLOPACITY = 386;

		public const short FILL__FILLBACKCOLOR = 387;

		public const short FILL__BACKOPACITY = 388;

		public const short FILL__CRMOD = 389;

		public const short FILL__PATTERNTEXTURE = 390;

		public const short FILL__BLIPFILENAME = 391;

		public const short FILL__BLIPFLAGS = 392;

		public const short FILL__WIDTH = 393;

		public const short FILL__HEIGHT = 394;

		public const short FILL__ANGLE = 395;

		public const short FILL__FOCUS = 396;

		public const short FILL__TOLEFT = 397;

		public const short FILL__TOTOP = 398;

		public const short FILL__TORIGHT = 399;

		public const short FILL__TOBOTTOM = 400;

		public const short FILL__RECTLEFT = 401;

		public const short FILL__RECTTOP = 402;

		public const short FILL__RECTRIGHT = 403;

		public const short FILL__RECTBOTTOM = 404;

		public const short FILL__DZTYPE = 405;

		public const short FILL__SHADEPRESet = 406;

		public const short FILL__SHADECOLORS = 407;

		public const short FILL__ORIGINX = 408;

		public const short FILL__ORIGINY = 409;

		public const short FILL__SHAPEORIGINX = 410;

		public const short FILL__SHAPEORIGINY = 411;

		public const short FILL__SHADETYPE = 412;

		public const short FILL__FILLED = 443;

		public const short FILL__HITTESTFILL = 444;

		public const short FILL__SHAPE = 445;

		public const short FILL__USERECT = 446;

		public const short FILL__NOFILLHITTEST = 447;

		public const short LINESTYLE__COLOR = 448;

		public const short LINESTYLE__OPACITY = 449;

		public const short LINESTYLE__BACKCOLOR = 450;

		public const short LINESTYLE__CRMOD = 451;

		public const short LINESTYLE__LINETYPE = 452;

		public const short LINESTYLE__FILLBLIP = 453;

		public const short LINESTYLE__FILLBLIPNAME = 454;

		public const short LINESTYLE__FILLBLIPFLAGS = 455;

		public const short LINESTYLE__FILLWIDTH = 456;

		public const short LINESTYLE__FILLHEIGHT = 457;

		public const short LINESTYLE__FILLDZTYPE = 458;

		public const short LINESTYLE__LINEWIDTH = 459;

		public const short LINESTYLE__LINEMITERLIMIT = 460;

		public const short LINESTYLE__LINESTYLE = 461;

		public const short LINESTYLE__LINEDASHING = 462;

		public const short LINESTYLE__LINEDASHSTYLE = 463;

		public const short LINESTYLE__LINESTARTARROWHEAD = 464;

		public const short LINESTYLE__LINEENDARROWHEAD = 465;

		public const short LINESTYLE__LINESTARTARROWWIDTH = 466;

		public const short LINESTYLE__LINEESTARTARROWLength = 467;

		public const short LINESTYLE__LINEENDARROWWIDTH = 468;

		public const short LINESTYLE__LINEENDARROWLength = 469;

		public const short LINESTYLE__LINEJOINSTYLE = 470;

		public const short LINESTYLE__LINEENDCAPSTYLE = 471;

		public const short LINESTYLE__ARROWHEADSOK = 507;

		public const short LINESTYLE__ANYLINE = 508;

		public const short LINESTYLE__HITLINETEST = 509;

		public const short LINESTYLE__LINEFILLSHAPE = 510;

		public const short LINESTYLE__NOLINEDRAWDASH = 511;

		public const short SHADOWSTYLE__TYPE = 512;

		public const short SHADOWSTYLE__COLOR = 513;

		public const short SHADOWSTYLE__HIGHLIGHT = 514;

		public const short SHADOWSTYLE__CRMOD = 515;

		public const short SHADOWSTYLE__OPACITY = 516;

		public const short SHADOWSTYLE__OFFSetX = 517;

		public const short SHADOWSTYLE__OFFSetY = 518;

		public const short SHADOWSTYLE__SECONDOFFSetX = 519;

		public const short SHADOWSTYLE__SECONDOFFSetY = 520;

		public const short SHADOWSTYLE__SCALEXTOX = 521;

		public const short SHADOWSTYLE__SCALEYTOX = 522;

		public const short SHADOWSTYLE__SCALEXTOY = 523;

		public const short SHADOWSTYLE__SCALEYTOY = 524;

		public const short SHADOWSTYLE__PERSPECTIVEX = 525;

		public const short SHADOWSTYLE__PERSPECTIVEY = 526;

		public const short SHADOWSTYLE__WEIGHT = 527;

		public const short SHADOWSTYLE__ORIGINX = 528;

		public const short SHADOWSTYLE__ORIGINY = 529;

		public const short SHADOWSTYLE__SHADOW = 574;

		public const short SHADOWSTYLE__SHADOWOBSURED = 575;

		public const short PERSPECTIVE__TYPE = 576;

		public const short PERSPECTIVE__OFFSetX = 577;

		public const short PERSPECTIVE__OFFSetY = 578;

		public const short PERSPECTIVE__SCALEXTOX = 579;

		public const short PERSPECTIVE__SCALEYTOX = 580;

		public const short PERSPECTIVE__SCALEXTOY = 581;

		public const short PERSPECTIVE__SCALEYTOY = 582;

		public const short PERSPECTIVE__PERSPECTIVEX = 583;

		public const short PERSPECTIVE__PERSPECTIVEY = 584;

		public const short PERSPECTIVE__WEIGHT = 585;

		public const short PERSPECTIVE__ORIGINX = 586;

		public const short PERSPECTIVE__ORIGINY = 587;

		public const short PERSPECTIVE__PERSPECTIVEON = 639;

		public const short THREED__SPECULARAMOUNT = 640;

		public const short THREED__DIFFUSEAMOUNT = 661;

		public const short THREED__SHININESS = 662;

		public const short THREED__EDGetHICKNESS = 663;

		public const short THREED__EXTRUDEFORWARD = 664;

		public const short THREED__EXTRUDEBACKWARD = 665;

		public const short THREED__EXTRUDEPLANE = 666;

		public const short THREED__EXTRUSIONCOLOR = 667;

		public const short THREED__CRMOD = 648;

		public const short THREED__3DEFFECT = 700;

		public const short THREED__METALLIC = 701;

		public const short THREED__USEEXTRUSIONCOLOR = 702;

		public const short THREED__LIGHTFACE = 703;

		public const short THREEDSTYLE__YROTATIONANGLE = 704;

		public const short THREEDSTYLE__XROTATIONANGLE = 705;

		public const short THREEDSTYLE__ROTATIONAXISX = 706;

		public const short THREEDSTYLE__ROTATIONAXISY = 707;

		public const short THREEDSTYLE__ROTATIONAXISZ = 708;

		public const short THREEDSTYLE__ROTATIONANGLE = 709;

		public const short THREEDSTYLE__ROTATIONCENTERX = 710;

		public const short THREEDSTYLE__ROTATIONCENTERY = 711;

		public const short THREEDSTYLE__ROTATIONCENTERZ = 712;

		public const short THREEDSTYLE__RENDERMODE = 713;

		public const short THREEDSTYLE__TOLERANCE = 714;

		public const short THREEDSTYLE__XVIEWPOINT = 715;

		public const short THREEDSTYLE__YVIEWPOINT = 716;

		public const short THREEDSTYLE__ZVIEWPOINT = 717;

		public const short THREEDSTYLE__ORIGINX = 718;

		public const short THREEDSTYLE__ORIGINY = 719;

		public const short THREEDSTYLE__SKEWANGLE = 720;

		public const short THREEDSTYLE__SKEWAMOUNT = 721;

		public const short THREEDSTYLE__AMBIENTINTENSITY = 722;

		public const short THREEDSTYLE__KEYX = 723;

		public const short THREEDSTYLE__KEYY = 724;

		public const short THREEDSTYLE__KEYZ = 725;

		public const short THREEDSTYLE__KEYINTENSITY = 726;

		public const short THREEDSTYLE__FillX = 727;

		public const short THREEDSTYLE__FillY = 728;

		public const short THREEDSTYLE__FillZ = 729;

		public const short THREEDSTYLE__FillINTENSITY = 730;

		public const short THREEDSTYLE__CONSTRAINROTATION = 763;

		public const short THREEDSTYLE__ROTATIONCENTERAUTO = 764;

		public const short THREEDSTYLE__PARALLEL = 765;

		public const short THREEDSTYLE__KEYHARSH = 766;

		public const short THREEDSTYLE__FillHARSH = 767;

		public const short SHAPE__MASTER = 769;

		public const short SHAPE__CONNECTORSTYLE = 771;

		public const short SHAPE__BLACKANDWHITESetTINGS = 772;

		public const short SHAPE__WMODEPUREBW = 773;

		public const short SHAPE__WMODEBW = 774;

		public const short SHAPE__OLEICON = 826;

		public const short SHAPE__PREFERRELATIVERESIZE = 827;

		public const short SHAPE__LOCKSHAPETYPE = 828;

		public const short SHAPE__DELETEATTACHEDOBJECT = 830;

		public const short SHAPE__BACKGROUNDSHAPE = 831;

		public const short CALLOUT__CALLOUTTYPE = 832;

		public const short CALLOUT__XYCALLOUTGAP = 833;

		public const short CALLOUT__CALLOUTANGLE = 834;

		public const short CALLOUT__CALLOUTDROPTYPE = 835;

		public const short CALLOUT__CALLOUTDROPSPECIFIED = 836;

		public const short CALLOUT__CALLOUTLENGTHSPECIFIED = 837;

		public const short CALLOUT__ISCALLOUT = 889;

		public const short CALLOUT__CALLOUTACCENTBAR = 890;

		public const short CALLOUT__CALLOUTTEXTBORDER = 891;

		public const short CALLOUT__CALLOUTMINUSX = 892;

		public const short CALLOUT__CALLOUTMINUSY = 893;

		public const short CALLOUT__DROPAUTO = 894;

		public const short CALLOUT__LENGTHSPECIFIED = 895;

		public const short GROUPSHAPE__SHAPENAME = 896;

		public const short GROUPSHAPE__DESCRIPTION = 897;

		public const short GROUPSHAPE__HYPERLINK = 898;

		public const short GROUPSHAPE__WRAPPOLYGONVERTICES = 899;

		public const short GROUPSHAPE__WRAPDISTLEFT = 900;

		public const short GROUPSHAPE__WRAPDISTTOP = 901;

		public const short GROUPSHAPE__WRAPDISTRIGHT = 902;

		public const short GROUPSHAPE__WRAPDISTBOTTOM = 903;

		public const short GROUPSHAPE__REGROUPID = 904;

		public const short GROUPSHAPE__UNUSED906 = 906;

		public const short GROUPSHAPE__TOOLTIP = 909;

		public const short GROUPSHAPE__SCRIPT = 910;

		public const short GROUPSHAPE__POSH = 911;

		public const short GROUPSHAPE__POSRELH = 912;

		public const short GROUPSHAPE__POSV = 913;

		public const short GROUPSHAPE__POSRELV = 914;

		public const short GROUPSHAPE__HR_PCT = 915;

		public const short GROUPSHAPE__HR_ALIGN = 916;

		public const short GROUPSHAPE__HR_HEIGHT = 917;

		public const short GROUPSHAPE__HR_WIDTH = 918;

		public const short GROUPSHAPE__SCRIPTEXT = 919;

		public const short GROUPSHAPE__SCRIPTLANG = 920;

		public const short GROUPSHAPE__BORDERTOPCOLOR = 923;

		public const short GROUPSHAPE__BORDERLEFTCOLOR = 924;

		public const short GROUPSHAPE__BORDERBOTTOMCOLOR = 925;

		public const short GROUPSHAPE__BORDERRIGHTCOLOR = 926;

		public const short GROUPSHAPE__TABLEPROPERTIES = 927;

		public const short GROUPSHAPE__TABLEROWPROPERTIES = 928;

		public const short GROUPSHAPE__WEBBOT = 933;

		public const short GROUPSHAPE__METROBLOB = 937;

		public const short GROUPSHAPE__ZORDER = 938;

		public const short GROUPSHAPE__FLAGS = 959;

		public const short GROUPSHAPE__EDITEDWRAP = 953;

		public const short GROUPSHAPE__BEHINDDOCUMENT = 954;

		public const short GROUPSHAPE__ONDBLCLICKNOTIFY = 955;

		public const short GROUPSHAPE__ISBUTTON = 956;

		public const short GROUPSHAPE__1DADJUSTMENT = 957;

		public const short GROUPSHAPE__HIDDEN = 958;

		public const short GROUPSHAPE__PRINT = 959;

		private static Hashtable properties;

		/// <summary>
		/// Inits the props.
		/// </summary>
		private static void InitProps()
		{
			if (properties == null)
			{
				properties = new Hashtable();
				AddProp(4, GetData("transform.rotation"));
				AddProp(119, GetData("protection.lockrotation"));
				AddProp(120, GetData("protection.lockaspectratio"));
				AddProp(121, GetData("protection.lockposition"));
				AddProp(122, GetData("protection.lockagainstselect"));
				AddProp(123, GetData("protection.lockcropping"));
				AddProp(124, GetData("protection.lockvertices"));
				AddProp(125, GetData("protection.locktext"));
				AddProp(126, GetData("protection.lockadjusthandles"));
				AddProp(127, GetData("protection.lockagainstgrouping", 1));
				AddProp(128, GetData("text.textid"));
				AddProp(129, GetData("text.textleft"));
				AddProp(130, GetData("text.texttop"));
				AddProp(131, GetData("text.textright"));
				AddProp(132, GetData("text.textbottom"));
				AddProp(133, GetData("text.wraptext"));
				AddProp(134, GetData("text.scaletext"));
				AddProp(135, GetData("text.anchortext"));
				AddProp(136, GetData("text.textflow"));
				AddProp(137, GetData("text.fontrotation"));
				AddProp(138, GetData("text.idofnextshape"));
				AddProp(139, GetData("text.bidir"));
				AddProp(187, GetData("text.singleclickselects"));
				AddProp(188, GetData("text.usehostmargins"));
				AddProp(189, GetData("text.rotatetextwithshape"));
				AddProp(190, GetData("text.sizeshapetofittext"));
				AddProp(191, GetData("text.sizetexttofitshape", 1));
				AddProp(192, GetData("geotext.unicode"));
				AddProp(193, GetData("geotext.rtftext"));
				AddProp(194, GetData("geotext.alignmentoncurve"));
				AddProp(195, GetData("geotext.defaultpointsize"));
				AddProp(196, GetData("geotext.textspacing"));
				AddProp(197, GetData("geotext.fontfamilyname"));
				AddProp(240, GetData("geotext.reverseroworder"));
				AddProp(241, GetData("geotext.hastexteffect"));
				AddProp(242, GetData("geotext.rotatecharacters"));
				AddProp(243, GetData("geotext.kerncharacters"));
				AddProp(244, GetData("geotext.tightortrack"));
				AddProp(245, GetData("geotext.stretchtofitshape"));
				AddProp(246, GetData("geotext.charboundingbox"));
				AddProp(247, GetData("geotext.scaletextonpath"));
				AddProp(248, GetData("geotext.stretchcharheight"));
				AddProp(249, GetData("geotext.nomeasurealongpath"));
				AddProp(250, GetData("geotext.boldfont"));
				AddProp(251, GetData("geotext.italicfont"));
				AddProp(252, GetData("geotext.underlinefont"));
				AddProp(253, GetData("geotext.shadowfont"));
				AddProp(254, GetData("geotext.smallcapsfont"));
				AddProp(255, GetData("geotext.strikethroughfont"));
				AddProp(256, GetData("blip.cropfromtop"));
				AddProp(257, GetData("blip.cropfrombottom"));
				AddProp(258, GetData("blip.cropfromleft"));
				AddProp(259, GetData("blip.cropfromright"));
				AddProp(260, GetData("blip.bliptodisplay"));
				AddProp(261, GetData("blip.blipfilename"));
				AddProp(262, GetData("blip.blipflags"));
				AddProp(263, GetData("blip.transparentcolor"));
				AddProp(264, GetData("blip.contrastSetting"));
				AddProp(265, GetData("blip.brightnessSetting"));
				AddProp(266, GetData("blip.gamma"));
				AddProp(267, GetData("blip.pictureid"));
				AddProp(268, GetData("blip.doublemod"));
				AddProp(269, GetData("blip.pictureFillmod"));
				AddProp(270, GetData("blip.pictureline"));
				AddProp(271, GetData("blip.printblip"));
				AddProp(272, GetData("blip.printblipfilename"));
				AddProp(273, GetData("blip.printflags"));
				AddProp(316, GetData("blip.nohittestpicture"));
				AddProp(317, GetData("blip.picturegray"));
				AddProp(318, GetData("blip.picturebilevel"));
				AddProp(319, GetData("blip.pictureactive"));
				AddProp(320, GetData("geometry.left"));
				AddProp(321, GetData("geometry.top"));
				AddProp(322, GetData("geometry.right"));
				AddProp(323, GetData("geometry.bottom"));
				AddProp(324, GetData("geometry.shapepath", 3));
				AddProp(325, GetData("geometry.vertices", 5));
				AddProp(326, GetData("geometry.segmentinfo", 5));
				AddProp(327, GetData("geometry.adjustvalue"));
				AddProp(328, GetData("geometry.adjust2value"));
				AddProp(329, GetData("geometry.adjust3value"));
				AddProp(330, GetData("geometry.adjust4value"));
				AddProp(331, GetData("geometry.adjust5value"));
				AddProp(332, GetData("geometry.adjust6value"));
				AddProp(333, GetData("geometry.adjust7value"));
				AddProp(334, GetData("geometry.adjust8value"));
				AddProp(335, GetData("geometry.adjust9value"));
				AddProp(336, GetData("geometry.adjust10value"));
				AddProp(378, GetData("geometry.shadowOK"));
				AddProp(379, GetData("geometry.3dok"));
				AddProp(380, GetData("geometry.lineok"));
				AddProp(381, GetData("geometry.geotextok"));
				AddProp(382, GetData("geometry.fillshadeshapeok"));
				AddProp(383, GetData("geometry.fillok", 1));
				AddProp(384, GetData("fill.filltype"));
				AddProp(385, GetData("fill.fillcolor", 2));
				AddProp(386, GetData("fill.fillopacity"));
				AddProp(387, GetData("fill.fillbackcolor", 2));
				AddProp(388, GetData("fill.backopacity"));
				AddProp(389, GetData("fill.crmod"));
				AddProp(390, GetData("fill.patterntexture"));
				AddProp(391, GetData("fill.blipfilename"));
				AddProp(392, GetData("fill.blipflags"));
				AddProp(393, GetData("fill.width"));
				AddProp(394, GetData("fill.height"));
				AddProp(395, GetData("fill.angle"));
				AddProp(396, GetData("fill.focus"));
				AddProp(397, GetData("fill.toleft"));
				AddProp(398, GetData("fill.totop"));
				AddProp(399, GetData("fill.toright"));
				AddProp(400, GetData("fill.tobottom"));
				AddProp(401, GetData("fill.rectleft"));
				AddProp(402, GetData("fill.recttop"));
				AddProp(403, GetData("fill.rectright"));
				AddProp(404, GetData("fill.rectbottom"));
				AddProp(405, GetData("fill.dztype"));
				AddProp(406, GetData("fill.shadepReset"));
				AddProp(407, GetData("fill.shadecolors", 5));
				AddProp(408, GetData("fill.originx"));
				AddProp(409, GetData("fill.originy"));
				AddProp(410, GetData("fill.shapeoriginx"));
				AddProp(411, GetData("fill.shapeoriginy"));
				AddProp(412, GetData("fill.shadetype"));
				AddProp(443, GetData("fill.filled"));
				AddProp(444, GetData("fill.hittestfill"));
				AddProp(445, GetData("fill.shape"));
				AddProp(446, GetData("fill.userect"));
				AddProp(447, GetData("fill.nofillhittest", 1));
				AddProp(448, GetData("linestyle.color", 2));
				AddProp(449, GetData("linestyle.opacity"));
				AddProp(450, GetData("linestyle.backcolor", 2));
				AddProp(451, GetData("linestyle.crmod"));
				AddProp(452, GetData("linestyle.linetype"));
				AddProp(453, GetData("linestyle.fillblip"));
				AddProp(454, GetData("linestyle.fillblipname"));
				AddProp(455, GetData("linestyle.fillblipflags"));
				AddProp(456, GetData("linestyle.fillwidth"));
				AddProp(457, GetData("linestyle.fillheight"));
				AddProp(458, GetData("linestyle.filldztype"));
				AddProp(459, GetData("linestyle.linewidth"));
				AddProp(460, GetData("linestyle.linemiterlimit"));
				AddProp(461, GetData("linestyle.linestyle"));
				AddProp(462, GetData("linestyle.linedashing"));
				AddProp(463, GetData("linestyle.linedashstyle", 5));
				AddProp(464, GetData("linestyle.linestartarrowhead"));
				AddProp(465, GetData("linestyle.lineendarrowhead"));
				AddProp(466, GetData("linestyle.linestartarrowwidth"));
				AddProp(467, GetData("linestyle.lineestartarrowlength"));
				AddProp(468, GetData("linestyle.lineendarrowwidth"));
				AddProp(469, GetData("linestyle.lineendarrowlength"));
				AddProp(470, GetData("linestyle.linejoinstyle"));
				AddProp(471, GetData("linestyle.lineendcapstyle"));
				AddProp(507, GetData("linestyle.arrowheadsok"));
				AddProp(508, GetData("linestyle.anyline"));
				AddProp(509, GetData("linestyle.hitlinetest"));
				AddProp(510, GetData("linestyle.linefillshape"));
				AddProp(511, GetData("linestyle.nolinedrawdash", 1));
				AddProp(512, GetData("shadowstyle.type"));
				AddProp(513, GetData("shadowstyle.color", 2));
				AddProp(514, GetData("shadowstyle.highlight"));
				AddProp(515, GetData("shadowstyle.crmod"));
				AddProp(516, GetData("shadowstyle.opacity"));
				AddProp(517, GetData("shadowstyle.offsetx"));
				AddProp(518, GetData("shadowstyle.offsety"));
				AddProp(519, GetData("shadowstyle.secondoffsetx"));
				AddProp(520, GetData("shadowstyle.secondoffsety"));
				AddProp(521, GetData("shadowstyle.scalextox"));
				AddProp(522, GetData("shadowstyle.scaleytox"));
				AddProp(523, GetData("shadowstyle.scalextoy"));
				AddProp(524, GetData("shadowstyle.scaleytoy"));
				AddProp(525, GetData("shadowstyle.perspectivex"));
				AddProp(526, GetData("shadowstyle.perspectivey"));
				AddProp(527, GetData("shadowstyle.weight"));
				AddProp(528, GetData("shadowstyle.originx"));
				AddProp(529, GetData("shadowstyle.originy"));
				AddProp(574, GetData("shadowstyle.shadow"));
				AddProp(575, GetData("shadowstyle.shadowobsured"));
				AddProp(576, GetData("perspective.type"));
				AddProp(577, GetData("perspective.offsetx"));
				AddProp(578, GetData("perspective.offsety"));
				AddProp(579, GetData("perspective.scalextox"));
				AddProp(580, GetData("perspective.scaleytox"));
				AddProp(581, GetData("perspective.scalextoy"));
				AddProp(582, GetData("perspective.scaleytoy"));
				AddProp(583, GetData("perspective.perspectivex"));
				AddProp(584, GetData("perspective.perspectivey"));
				AddProp(585, GetData("perspective.weight"));
				AddProp(586, GetData("perspective.originx"));
				AddProp(587, GetData("perspective.originy"));
				AddProp(639, GetData("perspective.perspectiveon"));
				AddProp(640, GetData("3d.specularamount"));
				AddProp(661, GetData("3d.diffuseamount"));
				AddProp(662, GetData("3d.shininess"));
				AddProp(663, GetData("3d.edGethickness"));
				AddProp(664, GetData("3d.extrudeforward"));
				AddProp(665, GetData("3d.extrudebackward"));
				AddProp(666, GetData("3d.extrudeplane"));
				AddProp(667, GetData("3d.extrusioncolor", 2));
				AddProp(648, GetData("3d.crmod"));
				AddProp(700, GetData("3d.3deffect"));
				AddProp(701, GetData("3d.metallic"));
				AddProp(702, GetData("3d.useextrusioncolor", 2));
				AddProp(703, GetData("3d.lightface"));
				AddProp(704, GetData("3dstyle.yrotationangle"));
				AddProp(705, GetData("3dstyle.xrotationangle"));
				AddProp(706, GetData("3dstyle.rotationaxisx"));
				AddProp(707, GetData("3dstyle.rotationaxisy"));
				AddProp(708, GetData("3dstyle.rotationaxisz"));
				AddProp(709, GetData("3dstyle.rotationangle"));
				AddProp(710, GetData("3dstyle.rotationcenterx"));
				AddProp(711, GetData("3dstyle.rotationcentery"));
				AddProp(712, GetData("3dstyle.rotationcenterz"));
				AddProp(713, GetData("3dstyle.rendermode"));
				AddProp(714, GetData("3dstyle.tolerance"));
				AddProp(715, GetData("3dstyle.xviewpoint"));
				AddProp(716, GetData("3dstyle.yviewpoint"));
				AddProp(717, GetData("3dstyle.zviewpoint"));
				AddProp(718, GetData("3dstyle.originx"));
				AddProp(719, GetData("3dstyle.originy"));
				AddProp(720, GetData("3dstyle.skewangle"));
				AddProp(721, GetData("3dstyle.skewamount"));
				AddProp(722, GetData("3dstyle.ambientintensity"));
				AddProp(723, GetData("3dstyle.keyx"));
				AddProp(724, GetData("3dstyle.keyy"));
				AddProp(725, GetData("3dstyle.keyz"));
				AddProp(726, GetData("3dstyle.keyintensity"));
				AddProp(727, GetData("3dstyle.fillx"));
				AddProp(728, GetData("3dstyle.filly"));
				AddProp(729, GetData("3dstyle.fillz"));
				AddProp(730, GetData("3dstyle.fillintensity"));
				AddProp(763, GetData("3dstyle.constrainrotation"));
				AddProp(764, GetData("3dstyle.rotationcenterauto"));
				AddProp(765, GetData("3dstyle.parallel"));
				AddProp(766, GetData("3dstyle.keyharsh"));
				AddProp(767, GetData("3dstyle.fillharsh"));
				AddProp(769, GetData("shape.master"));
				AddProp(771, GetData("shape.connectorstyle"));
				AddProp(772, GetData("shape.blackandwhiteSettings"));
				AddProp(773, GetData("shape.wmodepurebw"));
				AddProp(774, GetData("shape.wmodebw"));
				AddProp(826, GetData("shape.oleicon"));
				AddProp(827, GetData("shape.preferrelativeresize"));
				AddProp(828, GetData("shape.lockshapetype"));
				AddProp(830, GetData("shape.deleteattachedobject"));
				AddProp(831, GetData("shape.backgroundshape"));
				AddProp(832, GetData("callout.callouttype"));
				AddProp(833, GetData("callout.xycalloutgap"));
				AddProp(834, GetData("callout.calloutangle"));
				AddProp(835, GetData("callout.calloutdroptype"));
				AddProp(836, GetData("callout.calloutdropspecified"));
				AddProp(837, GetData("callout.calloutlengthspecified"));
				AddProp(889, GetData("callout.iscallout"));
				AddProp(890, GetData("callout.calloutaccentbar"));
				AddProp(891, GetData("callout.callouttextborder"));
				AddProp(892, GetData("callout.calloutminusx"));
				AddProp(893, GetData("callout.calloutminusy"));
				AddProp(894, GetData("callout.dropauto"));
				AddProp(895, GetData("callout.lengthspecified"));
				AddProp(896, GetData("groupshape.shapename"));
				AddProp(897, GetData("groupshape.description"));
				AddProp(898, GetData("groupshape.hyperlink"));
				AddProp(899, GetData("groupshape.wrappolygonvertices", 5));
				AddProp(900, GetData("groupshape.wrapdistleft"));
				AddProp(901, GetData("groupshape.wrapdisttop"));
				AddProp(902, GetData("groupshape.wrapdistright"));
				AddProp(903, GetData("groupshape.wrapdistbottom"));
				AddProp(904, GetData("groupshape.regroupid"));
				AddProp(953, GetData("groupshape.editedwrap"));
				AddProp(954, GetData("groupshape.behinddocument"));
				AddProp(955, GetData("groupshape.ondblclicknotify"));
				AddProp(956, GetData("groupshape.isbutton"));
				AddProp(957, GetData("groupshape.1dadjustment"));
				AddProp(958, GetData("groupshape.hidden"));
				AddProp(959, GetData("groupshape.print", 1));
			}
		}

		/// <summary>
		/// Adds the prop.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <param name="data">The data.</param>
		private static void AddProp(int s, EscherPropertyMetaData data)
		{
			properties[(short)s] = data;
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="propName">Name of the prop.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private static EscherPropertyMetaData GetData(string propName, byte type)
		{
			return new EscherPropertyMetaData(propName, type);
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="propName">Name of the prop.</param>
		/// <returns></returns>
		private static EscherPropertyMetaData GetData(string propName)
		{
			return new EscherPropertyMetaData(propName);
		}

		/// <summary>
		/// Gets the name of the property.
		/// </summary>
		/// <param name="propertyId">The property id.</param>
		/// <returns></returns>
		public static string GetPropertyName(short propertyId)
		{
			InitProps();
			EscherPropertyMetaData escherPropertyMetaData = (EscherPropertyMetaData)properties[propertyId];
			if (escherPropertyMetaData != null)
			{
				return escherPropertyMetaData.Description;
			}
			return "unknown";
		}

		/// <summary>
		/// Gets the type of the property.
		/// </summary>
		/// <param name="propertyId">The property id.</param>
		/// <returns></returns>
		public static byte GetPropertyType(short propertyId)
		{
			InitProps();
			return ((EscherPropertyMetaData)properties[propertyId])?.Type ?? 0;
		}
	}
}
