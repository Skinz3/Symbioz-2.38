using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights
{
    /// <summary>
    /// Ankama_UI => Tooltips > FormulasHandler.as
    /// </summary>
    public class ExperienceFormulas
    {

        static double[] XP_GROUP = new double[] { 1, 1.1, 1.5, 2.3, 3.1, 3.6, 4.2, 4.7, 4.9, 5.2, 6.3, 6.9 };

        private const double MAX_LEVEL_MALUS = 4;

        public double _xpSolo { get; set; }
        public double _xpGroup { get; set; }



        public double GetArenaMalusDrop(double param1, double param2)
        {
            double _loc3_ = Math.Round(100 - param1 / param2 * 100);
            return _loc3_ < 0 ? 0 : _loc3_;
        }
        private double truncate(double val)
        {
            double multiplier = Math.Pow(10, 0);
            double truncatedVal = (val * multiplier);
            return (truncatedVal / multiplier);
        }
        public void initXpFormula(PlayerData param1, IEnumerable<MonsterData> param2, IEnumerable<GroupMemberData> param3, double param4, double param5)
        {
            double _loc11_ = 0;
            double _loc12_ = 0;
            double _loc13_ = 0;
            double _loc15_ = 0;
            double _loc33_ = 0;
            double _loc34_ = 0;
            double _loc35_ = 0;
            double _loc36_ = 0;
            double _loc37_ = 0;
            double _loc6_ = 0;
            double _loc7_ = 0;
            double _loc8_ = 0;
            double _loc9_ = 0;

            foreach (var _loc10_ in param2)
            {
                _loc6_ = _loc6_ + _loc10_.xp;
                _loc8_ = _loc8_ + _loc10_.level;
                _loc9_ = _loc9_ + (_loc10_.hiddenLevel > 0 ? _loc10_.hiddenLevel : _loc10_.level);
                if (_loc10_.level > _loc7_)
                {
                    _loc7_ = _loc10_.level;
                }
                _loc11_ = 0;
                _loc12_ = 0;
                _loc13_ = 0;

                foreach (var _loc14_ in param3)
                {
                    _loc11_ = _loc11_ + _loc14_.level;
                    if (_loc14_.level > _loc12_)
                    {
                        _loc12_ = _loc14_.level;
                    }
                }
                foreach (var _loc14_ in param3)
                {
                    if (!_loc14_.companion && _loc14_.level >= _loc12_ / 3)
                    {
                        _loc13_++;
                    }
                }
                _loc15_ = 1;
                if (_loc11_ - 5 > _loc8_)
                {
                    _loc15_ = _loc8_ / _loc11_;
                }
                else if (_loc11_ + 10 < _loc8_)
                {
                    _loc15_ = (_loc11_ + 10) / _loc8_;
                }
                double _loc16_ = 1;
                if (param1.level - 5 > _loc8_)
                {
                    _loc16_ = _loc8_ / param1.level;
                }
                else if (param1.level + 10 < _loc8_)
                {
                    _loc16_ = (param1.level + 10) / _loc8_;
                }
                double _loc17_ = Math.Min(param1.level, this.truncate(2.5 * _loc7_));
                double _loc18_ = _loc17_ / param1.level * 100;
                double _loc19_ = _loc17_ / _loc11_ * 100;
                double _loc20_ = this.truncate(_loc6_ * XP_GROUP[0] * _loc16_);

                if (_loc13_ == 0)
                    _loc13_ = 1;

                double _loc21_ = this.truncate(_loc6_ * XP_GROUP[(int)(_loc13_ - 1)] * _loc15_);
                double _loc22_ = this.truncate(_loc18_ / 100 * _loc20_);
                double _loc23_ = this.truncate(_loc19_ / 100 * _loc21_);
                double _loc24_ = param4 <= 0 ? (double)1 : (double)(1 + param4 / 100);

                if (_loc12_ == 0)
                {
                    _loc12_ = param1.level;
                }
                double _loc25_ = Math.Min(MAX_LEVEL_MALUS, _loc9_ / (double)param2.Count() / _loc12_);

                _loc25_ = _loc25_ * _loc25_;

                double _loc26_ = this.truncate((100 + param1.level * 2.5) * this.truncate(param5 * _loc25_) / 100);
                double _loc27_ = Math.Max(param1.wisdom + _loc26_, 0);
                double _loc28_ = this.truncate(this.truncate(_loc22_ * (100 + _loc27_) / 100) * _loc24_);
                double _loc29_ = this.truncate(this.truncate(_loc23_ * (100 + _loc27_) / 100) * _loc24_);
                double _loc30_ = 1 + param1.xpBonusPercent / 100;
                double _loc31_ = _loc28_;
                double _loc32_ = _loc29_;

                if (param1.xpRatioMount > 0)
                {
                    _loc33_ = _loc31_ * param1.xpRatioMount / 100;
                    _loc34_ = _loc32_ * param1.xpRatioMount / 100;
                    _loc31_ = this.truncate(_loc31_ - _loc33_);
                    _loc32_ = this.truncate(_loc32_ - _loc34_);
                }
                _loc31_ = _loc31_ * _loc30_;
                _loc32_ = _loc32_ * _loc30_;

                if (param1.xpGuildGivenPercent > 0)
                {
                    _loc35_ = _loc31_ * param1.xpGuildGivenPercent / 100;
                    _loc36_ = _loc32_ * param1.xpGuildGivenPercent / 100;
                    _loc31_ = _loc31_ - _loc35_;
                    _loc32_ = _loc32_ - _loc36_;
                }

                if (param1.xpAlliancePrismBonusPercent > 0)
                {
                    _loc37_ = 1 + param1.xpAlliancePrismBonusPercent / 100;
                    _loc31_ = _loc31_ * _loc37_;
                    _loc32_ = _loc32_ * _loc37_;
                }
                _loc28_ = this.truncate(_loc31_);
                _loc29_ = this.truncate(_loc32_);
                this._xpSolo = _loc6_ > 0 ? (double)Math.Max(_loc28_, 1) : (double)0;
                this._xpGroup = _loc6_ > 0 ? (double)Math.Max(_loc29_, 1) : (double)0;

                this._xpSolo *= param1.expMul;
                this._xpGroup *= param1.expMul;


            }


        }
        public class MonsterData
        {
            public MonsterData(int level, int xp)
            {
                this.xp = xp;
                this.level = level;
            }

            public int xp;

            public int level;

            public int hiddenLevel;
        }
        public class GroupMemberData
        {
            public GroupMemberData(int level, bool isCompanion)
            {
                this.level = level;
                this.companion = isCompanion;
            }

            public int level;

            public bool companion;
        }
        public class PlayerData
        {
            public PlayerData(int level, int wisdom, int expMul)
            {
                this.level = level;
                this.wisdom = wisdom;
                this.xpBonusPercent = 0;
                this.xpRatioMount = 0;
                this.xpGuildGivenPercent = 0;
                this.xpAlliancePrismBonusPercent = 0;
                this.expMul = expMul;
            }

            public int level;

            public int wisdom;

            public int xpBonusPercent;

            public int xpRatioMount;

            public int xpGuildGivenPercent;

            public int xpAlliancePrismBonusPercent;

            public int expMul;
        }
    }

}