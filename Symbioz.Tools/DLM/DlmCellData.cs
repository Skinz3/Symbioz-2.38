using SSync.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmCellData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DlmMap _Map;



        public DlmMap Map
        {
            get
            {
                return this._Map;
            }
            private set
            {
                if (this._Map == value)
                {
                    return;
                }
                this._Map = value;
                this.OnPropertyChanged("Map");
            }
        }

        private short _Id;

        public short Id
        {
            get
            {
                return this._Id;
            }

            private set
            {
                this._Id = value;
                this.OnPropertyChanged("Id");
            }
        }

        private short _Floor;

        public short Floor
        {
            get
            {
                return this._Floor;
            }

            set
            {
                this._Floor = value;
                this.OnPropertyChanged("Floor");
            }
        }

        private short _LosMov;

        public short LosMov
        {
            get
            {
                return this._LosMov;
            }

            set
            {
                this._LosMov = value;
                this.OnPropertyChanged("Los");
                this.OnPropertyChanged("Mov");
                this.OnPropertyChanged("NonWalkableDuringFight");
                this.OnPropertyChanged("Red");
                this.OnPropertyChanged("Blue");
                this.OnPropertyChanged("FarmCell");
                this.OnPropertyChanged("Visible");
                this.OnPropertyChanged("LosMov");
            }
        }

        private byte _Speed;

        public byte Speed
        {
            get
            {
                return this._Speed;
            }

            set
            {
                this._Speed = value;
                this.OnPropertyChanged("Speed");
            }
        }

        private byte _MapChangeData;

        public byte MapChangeData
        {
            get
            {
                return this._MapChangeData;
            }

            set
            {
                this._MapChangeData = value;
                this.OnPropertyChanged("MapChangeData");
            }
        }

        private byte _MoveZone;

        public byte MoveZone
        {
            get
            {
                return this._MoveZone;
            }

            set
            {
                this._MoveZone = value;
                this.OnPropertyChanged("MoveZone");
            }
        }

        public bool Los;

        public bool Mov;

        public bool NonWalkableDuringFight;

        public bool NonWalkableDuringRP;

        public bool Red;

        public bool Blue;

        public bool FarmCell;

        public bool Visible;

        public bool HavenbagCell;

        public int Arrow;

        public DlmCellData(DlmMap map, short id)
        {
            this.Map = map;
            this.Id = id;
            this.LosMov = 3;
        }

        public static DlmCellData ReadFromStream(DlmMap map, short id, BigEndianReader reader)
        {
            DlmCellData cell = new DlmCellData(map, id);
            cell.Floor = (short)(reader.ReadByte() * 10);
            if (cell.Floor == -1280)
            {
                return cell;
            }
            if (map.Version >= 9)
            {
                cell.LosMov = reader.ReadShort();
                cell.Mov = (cell.LosMov & 1) == 0;
                cell.NonWalkableDuringFight = (cell.LosMov & 2) != 0;
                cell.NonWalkableDuringRP = (cell.LosMov & 4) != 0;
                cell.Los = (cell.LosMov & 8) == 0;
                cell.Blue = (cell.LosMov & 16) != 0;
                cell.Red = (cell.LosMov & 32) != 0;
                cell.Visible = (cell.LosMov & 64) != 0;
                cell.FarmCell = (cell.LosMov & 128) != 0;

                bool topArrow;
                bool bottomArrow;
                bool rightArrow;
                bool leftArrow;
                if (map.Version >= 10)
                {
                    cell.HavenbagCell = (cell.LosMov & 256) != 0;
                    topArrow = (cell.LosMov & 512) != 0;
                    bottomArrow = (cell.LosMov & 1024) != 0;
                    rightArrow = (cell.LosMov & 2048) != 0;
                    leftArrow = (cell.LosMov & 4096) != 0;
                }
                else
                {
                    topArrow = (cell.LosMov & 256) != 0;
                    bottomArrow = (cell.LosMov & 512) != 0;
                    rightArrow = (cell.LosMov & 1024) != 0;
                    leftArrow = (cell.LosMov & 2048) != 0;
                }
                if (topArrow)
                {
                    map.TopArrowCells.Add(cell.Id);
                }
                if (bottomArrow)
                {
                    map.BottomArrowCells.Add(cell.Id);
                }
                if (rightArrow)
                {
                    map.RightArrowCells.Add(cell.Id);
                }
                if (leftArrow)
                {
                    map.LeftArrowCells.Add(cell.Id);
                }
            }
            else
            {
                cell.LosMov = (byte)reader.ReadSByte();
                cell.Los = (cell.LosMov & 2) >> 1 == 1;
                cell.Mov = (cell.LosMov & 1) == 1;
                cell.Visible = (cell.LosMov & 64) >> 6 == 1;
                cell.FarmCell = (cell.LosMov & 32) >> 5 == 1;
                cell.Blue = (cell.LosMov & 16) >> 4 == 1;
                cell.Red = (cell.LosMov & 8) >> 3 == 1;
                cell.NonWalkableDuringRP = (cell.LosMov & 128) >> 7 == 1;
                cell.NonWalkableDuringFight = (cell.LosMov & 4) >> 2 == 1;
            }
          
            cell.Speed = reader.ReadByte();
          
            cell.MapChangeData = reader.ReadByte();
            
            if (map.Version > 5)
            {
                cell.MoveZone = (byte)reader.ReadSByte();
            }

            if (map.Version > 7 && map.Version < 9)
            {
                var tmpBits = reader.ReadByte();
                cell.Arrow = 15 & tmpBits;
                //if(cell.use)
                //{
                //   map.topArrowCell.push(id);
                //}
                //if(useBottomArrow)
                //{
                //   map.bottomArrowCell.push(id);
                //}
                //if(useLeftArrow)
                //{
                //   map.leftArrowCell.push(id);
                //}
                //if(useRightArrow)
                //{
                //   map.rightArrowCell.push(id);
                //}
            }
            return cell;
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}