using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public enum ST_ConstraintType
	{
		none,
		alignOff,
		begMarg,
		bendDist,
		begPad,
		b,
		bMarg,
		bOff,
		ctrX,
		ctrXOff,
		ctrY,
		ctrYOff,
		connDist,
		diam,
		endMarg,
		endPad,
		h,
		hArH,
		hOff,
		l,
		lMarg,
		lOff,
		r,
		rMarg,
		rOff,
		primFontSz,
		pyraAcctRatio,
		secFontSz,
		sibSp,
		secSibSp,
		sp,
		stemThick,
		t,
		tMarg,
		tOff,
		userA,
		userB,
		userC,
		userD,
		userE,
		userF,
		userG,
		userH,
		userI,
		userJ,
		userK,
		userL,
		userM,
		userN,
		userO,
		userP,
		userQ,
		userR,
		userS,
		userT,
		userU,
		userV,
		userW,
		userX,
		userY,
		userZ,
		w,
		wArH,
		wOff
	}
}