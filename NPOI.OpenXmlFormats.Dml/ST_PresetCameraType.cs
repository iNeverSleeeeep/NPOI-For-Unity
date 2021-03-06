using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_PresetCameraType
	{
		legacyObliqueTopLeft,
		legacyObliqueTop,
		legacyObliqueTopRight,
		legacyObliqueLeft,
		legacyObliqueFront,
		legacyObliqueRight,
		legacyObliqueBottomLeft,
		legacyObliqueBottom,
		legacyObliqueBottomRight,
		legacyPerspectiveTopLeft,
		legacyPerspectiveTop,
		legacyPerspectiveTopRight,
		legacyPerspectiveLeft,
		legacyPerspectiveFront,
		legacyPerspectiveRight,
		legacyPerspectiveBottomLeft,
		legacyPerspectiveBottom,
		legacyPerspectiveBottomRight,
		orthographicFront,
		isometricTopUp,
		isometricTopDown,
		isometricBottomUp,
		isometricBottomDown,
		isometricLeftUp,
		isometricLeftDown,
		isometricRightUp,
		isometricRightDown,
		isometricOffAxis1Left,
		isometricOffAxis1Right,
		isometricOffAxis1Top,
		isometricOffAxis2Left,
		isometricOffAxis2Right,
		isometricOffAxis2Top,
		isometricOffAxis3Left,
		isometricOffAxis3Right,
		isometricOffAxis3Bottom,
		isometricOffAxis4Left,
		isometricOffAxis4Right,
		isometricOffAxis4Bottom,
		obliqueTopLeft,
		obliqueTop,
		obliqueTopRight,
		obliqueLeft,
		obliqueRight,
		obliqueBottomLeft,
		obliqueBottom,
		obliqueBottomRight,
		perspectiveFront,
		perspectiveLeft,
		perspectiveRight,
		perspectiveAbove,
		perspectiveBelow,
		perspectiveAboveLeftFacing,
		perspectiveAboveRightFacing,
		perspectiveContrastingLeftFacing,
		perspectiveContrastingRightFacing,
		perspectiveHeroicLeftFacing,
		perspectiveHeroicRightFacing,
		perspectiveHeroicExtremeLeftFacing,
		perspectiveHeroicExtremeRightFacing,
		perspectiveRelaxed,
		perspectiveRelaxedModerately
	}
}
