using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class Zone : IShape
    {
        private IShape m_shape;
        private char m_shapeType;
        private byte m_radius;
        private DirectionsEnum m_direction;
        public char ShapeType
        {
            get
            {
                return this.m_shapeType;
            }
            set
            {
                this.m_shapeType = value;
                this.InitializeShape();
            }
        }
        public IShape Shape
        {
            get
            {
                return this.m_shape;
            }
        }
        public uint Surface
        {
            get
            {
                return this.m_shape.Surface;
            }
        }
        public byte MinRadius
        {
            get
            {
                return this.m_shape.MinRadius;
            }
            set
            {
                this.m_shape.MinRadius = value;
            }
        }
        public DirectionsEnum Direction
        {
            get
            {
                return this.m_direction;
            }
            set
            {
                this.m_direction = value;
                if (this.m_shape != null)
                {
                    this.m_shape.Direction = value;
                }
            }
        }
        public byte Radius
        {
            get
            {
                return this.m_radius;
            }
            set
            {
                this.m_radius = value;
                if (this.m_shape != null)
                {
                    this.m_shape.Radius = value;
                }
            }
        }
        public Zone(char shape, byte radius)
        {
            this.Radius = radius;
            this.ShapeType = shape;
        }
        public Zone(char shape, byte radius, DirectionsEnum direction)
        {
            this.Radius = radius;
            this.Direction = direction;
            this.ShapeType = shape;
        }
        public short[] GetCells(short centerCell, MapRecord map)
        {
            return this.m_shape.GetCells(centerCell, map);
        }
        private void InitializeShape()
        {
            char shapeType = this.ShapeType;
            if (shapeType <= '+')
            {
                if (shapeType == '#')
                {
                    this.m_shape = new Cross(1, this.Radius)
                    {
                        Diagonal = true
                    };
                    goto end;
                }
                switch (shapeType)
                {
                    case '*':
                        this.m_shape = new Cross(0, this.Radius)
                        {
                            AllDirections = true
                        };
                        goto end;
                    case '+':
                        this.m_shape = new Cross(0, this.Radius)
                        {
                            Diagonal = true
                        };
                        goto end;
                }
            }
            else
            {
                if (shapeType == '?') // Slash
                {
                    this.m_shape = new Line(this.Radius);
                    goto end;
                }
                switch (shapeType)
                {
                        case '-':
                        this.m_shape = new Cross(0, Radius)
                        {
                            OnlyPerpendicular = true,
                            Diagonal = true,
                        };
                        goto end;
                    case 'a':
                        this.m_shape = new All();
                        goto end;
                    case 'A':
                        this.m_shape = new Lozenge(0, 63);
                        goto end;
                    case 'C':
                        this.m_shape = new Lozenge(0, this.Radius);
                        goto end;
                    case 'D':
                        this.m_shape = new Cross(0, this.Radius);
                        goto end;
                    case 'I':
                        this.m_shape = new Lozenge(this.Radius, 63);
                        goto end;
                    case 'L':
                        this.m_shape = new Line(this.Radius);
                        goto end;
                    case 'O':
                        this.m_shape = new Cross(1, this.Radius);
                        goto end;
                    case 'P':
                        this.m_shape = new Single();
                        goto end;
                    case 'Q':
                        this.m_shape = new Cross(1, this.Radius);
                        goto end;
                    case 'T':
                        this.m_shape = new Cross(0, this.Radius)
                        {
                            OnlyPerpendicular = true
                        };
                        goto end;
                    case 'U':
                        this.m_shape = new HalfLozenge(0, this.Radius);
                        goto end;
                    case 'V':
                        this.m_shape = new Cone(0, this.Radius);
                        goto end;
                    case 'W':// ?
                        this.m_shape = new Square(0, this.Radius);
                        goto end;
                    case 'G':  
                        this.m_shape = new Square(0, this.Radius);
                        goto end;
                    case 'X':
                        this.m_shape = new Cross(0, this.Radius);
                        goto end;
                }
            }
            this.m_shape = new Cross(0, 0);
        end:
            this.m_shape.Direction = this.Direction;
        }
    }
}
