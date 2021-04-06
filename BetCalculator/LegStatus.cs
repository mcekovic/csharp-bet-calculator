using System;
using BetCalculator.Rules;
using static BetCalculator.Rules.EachWayType;

namespace BetCalculator
{
   public class LegStatus
   {
      public static readonly LegStatus Open = new(1.0m, 0.0m, 1.0m, false);
      public static readonly LegStatus Won = new(1.0m, 0.0m, 1.0m, true);
      public static readonly LegStatus Void = new(0.0m, 1.0m, 1.0m, true);
      public static readonly LegStatus Lost = new(0.0m, 0.0m, 1.0m, true);
      
      public static LegStatus OpenEW(decimal placeOddsFactor) =>
         new(1.0m, 0.0m, placeOddsFactor, false);
      
      private readonly decimal _winFactor;
      private readonly decimal _voidFactor;
      private readonly decimal _placeOddsFactor;
      private readonly BetState _state;

      public LegStatus(decimal winFactor, decimal voidFactor, decimal placeOddsFactor = 1.0m, bool resulted = false)
      {
         if (winFactor < 0.0m)
            throw new ArgumentException("winFactor must not be negative");
         if (voidFactor < 0.0m)
            throw new ArgumentException("voidFactor must not be negative");
         if (placeOddsFactor < 0.0m)
            throw new ArgumentException("placeOddsFactor must not be negative");
         _winFactor = winFactor;
         _voidFactor = voidFactor;
         _placeOddsFactor = placeOddsFactor;
         if (!resulted)
            _state = BetState.Open;
         else if (winFactor > 0.0m)
            _state = BetState.Won;
         else if (voidFactor > 0.0m)
            _state = BetState.Void;
         else
            _state = BetState.Lost;
      }

      public decimal WinFactor => _winFactor;

      public decimal VoidFactor => _voidFactor;

      public decimal PlaceOddsFactor => _placeOddsFactor;

      public BetState State => _state;

      public decimal FactoredPrice(decimal price, EachWayType eachWayType)
         => _winFactor * ApplyOddsFactor(price, eachWayType) + _voidFactor;

      private decimal ApplyOddsFactor(decimal price, EachWayType eachWayType)
         => eachWayType switch
         {
            Win => price,
            Place => 1.0m + (price - 1.0m) * _placeOddsFactor,
            _ => throw new ArgumentOutOfRangeException(nameof(eachWayType), eachWayType, "Invalid eachWayType for LegStatus")
         };
   }
}