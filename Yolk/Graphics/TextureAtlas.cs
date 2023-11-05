using Microsoft.Xna.Framework.Graphics;
using System;
using Yolk.Geometry;
using Yolk.Geometry.Shapes;

namespace Yolk.Graphics
{
    public sealed class TextureAtlas
    {
        private readonly Rectangle[][] bounds;

        public TextureAtlas(Texture2D atlas, Point sprite, Point gap)
        {
            Texture = atlas;
            bounds = FindBounds(atlas, sprite, gap);
        }

        public TextureAtlas(Texture2D atlas,
                            int spriteWidth, int spriteHeight,
                            int horizontalGap, int verticalGap)
            : this(atlas,
                   new Point(spriteWidth, spriteHeight),
                   new Point(horizontalGap, verticalGap))
        {
        }

        public Rectangle this[int row, int column]
        {
            get
            {
                if (row >= Rows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row), "The referenced row is out of scope."
                    );
                }
                if (column >= Columns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column), "The referenced column is out of scope."
                    );
                }
                return bounds[row][column];
            }
        }

        public Rectangle this[Point point] => this[point.X, point.Y];

        public Rectangle[] this[int row]
        {
            get
            {
                if (row >= Rows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row), "The referenced row is out of scope."
                    );
                }
                return bounds[row];
            }
        }

        public Texture2D Texture { get; }

        public int Rows => bounds.GetLength(0);

        public int Columns => bounds.GetLength(1);

        private static Rectangle[][] FindBounds(Texture2D atlas, Point sprite, Point gap)
        {
            Point dimension = NumberOfSprites(atlas, sprite, gap);
            Point offset = sprite + gap;
            Point min = Point.Origin;

            Rectangle[][] bounds = new Rectangle[dimension.Y][];

            for (int row = 0; row < dimension.Y; ++row)
            {
                Rectangle[] rowOfBounds = new Rectangle[dimension.X];
                min.X = 0;

                for (int column = 0; column < dimension.X; ++column)
                {
                    rowOfBounds[column] = new Rectangle(min, sprite.X, sprite.Y);
                    min.X += offset.X;
                }

                bounds[row] = rowOfBounds;
                min.Y += offset.Y;
            }
            return bounds;
        }

        private static Point NumberOfSprites(Texture2D atlas, Point sprite, Point gap)
        {
            return new Point(
                NumberOfSprites(atlas.Width, sprite.X, gap.X),
                NumberOfSprites(atlas.Height, sprite.Y, gap.Y)
            );
        }

        private static int NumberOfSprites(int atlas, int sprite, int gap)
        {
            if (atlas <= 0)
            {
                return 0;
            }

            int offset = sprite + gap;
            int numberOfSprites = 1;

            atlas -= sprite;
            while (atlas > 0)
            {
                atlas -= offset;
                numberOfSprites++;
            }
            return numberOfSprites;
        }

        public Rectangle GetBound(int row, int column) => this[row, column];

        public Rectangle GetBound(Point point) => this[point];

        public Rectangle[] GetBounds(int row) => this[row];
    }
}
