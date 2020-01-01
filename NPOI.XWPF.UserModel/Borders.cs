namespace NPOI.XWPF.UserModel
{
	/// Specifies all types of borders which can be specified for WordProcessingML
	/// objects which have a border. Borders can be Separated into two types:
	/// <ul>
	/// <li> Line borders: which specify a pattern to be used when Drawing a line around the
	/// specified object.
	/// </li>
	/// <li> Art borders: which specify a repeated image to be used
	/// when Drawing a border around the specified object. Line borders may be
	/// specified on any object which allows a border, however, art borders may only
	/// be used as a border at the page level - the borders under the pgBorders
	/// element
	///             </li>
	/// </ul>
	/// @author Gisella Bronzetti
	public enum Borders
	{
		NIL = 1,
		NONE,
		/// Specifies a line border consisting of a single line around the parent
		/// object.
		SINGLE,
		THICK,
		DOUBLE,
		DOTTED,
		DASHED,
		DOT_DASH,
		DOT_DOT_DASH,
		TRIPLE,
		THIN_THICK_SMALL_GAP,
		THICK_THIN_SMALL_GAP,
		THIN_THICK_THIN_SMALL_GAP,
		THIN_THICK_MEDIUM_GAP,
		THICK_THIN_MEDIUM_GAP,
		THIN_THICK_THIN_MEDIUM_GAP,
		THIN_THICK_LARGE_GAP,
		THICK_THIN_LARGE_GAP,
		THIN_THICK_THIN_LARGE_GAP,
		WAVE,
		DOUBLE_WAVE,
		DASH_SMALL_GAP,
		DASH_DOT_STROKED,
		THREE_D_EMBOSS,
		THREE_D_ENGRAVE,
		OUTSET,
		INSET,
		/// Specifies an art border consisting of a repeated image of an apple
		APPLES,
		/// Specifies an art border consisting of a repeated image of a shell pattern
		ARCHED_SCALLOPS,
		/// Specifies an art border consisting of a repeated image of a baby pacifier
		BABY_PACIFIER,
		/// Specifies an art border consisting of a repeated image of a baby rattle
		BABY_RATTLE,
		/// Specifies an art border consisting of a repeated image of a Set of
		/// balloons
		BALLOONS_3_COLORS,
		/// Specifies an art border consisting of a repeated image of a hot air
		/// balloon
		BALLOONS_HOT_AIR,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background.
		BASIC_BLACK_DASHES,
		/// Specifies an art border consisting of a repeating image of a black dot on
		/// a white background.
		BASIC_BLACK_DOTS,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background
		BASIC_BLACK_SQUARES,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background.
		BASIC_THIN_LINES,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background.
		BASIC_WHITE_DASHES,
		/// Specifies an art border consisting of a repeating image of a white dot on
		/// a black background.
		BASIC_WHITE_DOTS,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background.
		BASIC_WHITE_SQUARES,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background.
		BASIC_WIDE_INLINE,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background
		BASIC_WIDE_MIDLINE,
		/// Specifies an art border consisting of a repeating image of a black and
		/// white background
		BASIC_WIDE_OUTLINE,
		/// Specifies an art border consisting of a repeated image of bats
		BATS,
		/// Specifies an art border consisting of repeating images of birds
		BIRDS,
		/// Specifies an art border consisting of a repeated image of birds flying
		BIRDS_FLIGHT,
		/// Specifies an art border consisting of a repeated image of a cabin
		CABINS,
		/// Specifies an art border consisting of a repeated image of a piece of cake
		CAKE_SLICE,
		/// Specifies an art border consisting of a repeated image of candy corn
		CANDY_CORN,
		/// Specifies an art border consisting of a repeated image of a knot work
		/// pattern
		CELTIC_KNOTWORK,
		/// Specifies an art border consisting of a banner.
		/// <p>
		/// If the border is on the left or right, no border is displayed.
		/// </p>
		CERTIFICATE_BANNER,
		/// Specifies an art border consisting of a repeating image of a chain link
		/// pattern.
		CHAIN_LINK,
		/// Specifies an art border consisting of a repeated image of a champagne
		/// bottle
		CHAMPAGNE_BOTTLE,
		/// Specifies an art border consisting of repeating images of a compass
		CHECKED_BAR_BLACK,
		/// Specifies an art border consisting of a repeating image of a colored
		/// pattern.
		CHECKED_BAR_COLOR,
		/// Specifies an art border consisting of a repeated image of a Checkerboard
		CHECKERED,
		/// Specifies an art border consisting of a repeated image of a Christmas
		/// tree
		CHRISTMAS_TREE,
		/// Specifies an art border consisting of repeating images of lines and
		/// circles
		CIRCLES_LINES,
		/// Specifies an art border consisting of a repeated image of a rectangular
		/// pattern
		CIRCLES_RECTANGLES,
		/// Specifies an art border consisting of a repeated image of a wave
		CLASSICAL_WAVE,
		/// Specifies an art border consisting of a repeated image of a clock
		CLOCKS,
		/// Specifies an art border consisting of repeating images of a compass
		COMPASS,
		/// Specifies an art border consisting of a repeated image of confetti
		CONFETTI,
		/// Specifies an art border consisting of a repeated image of confetti
		CONFETTI_GRAYS,
		/// Specifies an art border consisting of a repeated image of confetti
		CONFETTI_OUTLINE,
		/// Specifies an art border consisting of a repeated image of confetti
		/// streamers
		CONFETTI_STREAMERS,
		/// Specifies an art border consisting of a repeated image of confetti
		CONFETTI_WHITE,
		/// Specifies an art border consisting of a repeated image
		CORNER_TRIANGLES,
		/// Specifies an art border consisting of a dashed line
		COUPON_CUTOUT_DASHES,
		/// Specifies an art border consisting of a dotted line
		COUPON_CUTOUT_DOTS,
		/// Specifies an art border consisting of a repeated image of a maze-like
		/// pattern
		CRAZY_MAZE,
		/// Specifies an art border consisting of a repeated image of a butterfly
		CREATURES_BUTTERFLY,
		/// Specifies an art border consisting of a repeated image of a fish
		CREATURES_FISH,
		/// Specifies an art border consisting of repeating images of insects.
		CREATURES_INSECTS,
		/// Specifies an art border consisting of a repeated image of a ladybug
		CREATURES_LADY_BUG,
		/// Specifies an art border consisting of repeating images of a cross-stitch
		/// pattern
		CROSS_STITCH,
		/// Specifies an art border consisting of a repeated image of Cupid
		CUP,
		DECO_ARCH,
		DECO_ARCH_COLOR,
		DECO_BLOCKS,
		DIAMONDS_GRAY,
		DOUBLE_D,
		DOUBLE_DIAMONDS,
		EARTH_1,
		EARTH_2,
		ECLIPSING_SQUARES_1,
		ECLIPSING_SQUARES_2,
		EGGS_BLACK,
		FANS,
		FILM,
		FIRECRACKERS,
		FLOWERS_BLOCK_PRINT,
		FLOWERS_DAISIES,
		FLOWERS_MODERN_1,
		FLOWERS_MODERN_2,
		FLOWERS_PANSY,
		FLOWERS_RED_ROSE,
		FLOWERS_ROSES,
		FLOWERS_TEACUP,
		FLOWERS_TINY,
		GEMS,
		GINGERBREAD_MAN,
		GRADIENT,
		HANDMADE_1,
		HANDMADE_2,
		HEART_BALLOON,
		HEART_GRAY,
		HEARTS,
		HEEBIE_JEEBIES,
		HOLLY,
		HOUSE_FUNKY,
		HYPNOTIC,
		ICE_CREAM_CONES,
		LIGHT_BULB,
		LIGHTNING_1,
		LIGHTNING_2,
		MAP_PINS,
		MAPLE_LEAF,
		MAPLE_MUFFINS,
		MARQUEE,
		MARQUEE_TOOTHED,
		MOONS,
		MOSAIC,
		MUSIC_NOTES,
		NORTHWEST,
		OVALS,
		PACKAGES,
		PALMS_BLACK,
		PALMS_COLOR,
		PAPER_CLIPS,
		PAPYRUS,
		PARTY_FAVOR,
		PARTY_GLASS,
		PENCILS,
		PEOPLE,
		PEOPLE_WAVING,
		PEOPLE_HATS,
		POINSETTIAS,
		POSTAGE_STAMP,
		PUMPKIN_1,
		PUSH_PIN_NOTE_2,
		PUSH_PIN_NOTE_1,
		PYRAMIDS,
		PYRAMIDS_ABOVE,
		QUADRANTS,
		RINGS,
		SAFARI,
		SAWTOOTH,
		SAWTOOTH_GRAY,
		SCARED_CAT,
		SEATTLE,
		SHADOWED_SQUARES,
		SHARKS_TEETH,
		SHOREBIRD_TRACKS,
		SKYROCKET,
		SNOWFLAKE_FANCY,
		SNOWFLAKES,
		SOMBRERO,
		SOUTHWEST,
		STARS,
		STARS_TOP,
		STARS_3_D,
		STARS_BLACK,
		STARS_SHADOWED,
		SUN,
		SWIRLIGIG,
		TORN_PAPER,
		TORN_PAPER_BLACK,
		TREES,
		TRIANGLE_PARTY,
		TRIANGLES,
		TRIBAL_1,
		TRIBAL_2,
		TRIBAL_3,
		TRIBAL_4,
		TRIBAL_5,
		TRIBAL_6,
		TWISTED_LINES_1,
		TWISTED_LINES_2,
		VINE,
		WAVELINE,
		WEAVING_ANGLES,
		WEAVING_BRAID,
		WEAVING_RIBBON,
		WEAVING_STRIPS,
		WHITE_FLOWERS,
		WOODWORK,
		X_ILLUSIONS,
		ZANY_TRIANGLES,
		ZIG_ZAG,
		ZIG_ZAG_STITCH
	}
}
