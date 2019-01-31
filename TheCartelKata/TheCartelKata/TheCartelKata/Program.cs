using System;
using System.Collections.Generic;

namespace TheCartelKata
{
    /*
     * An infamous drug cartel have contacted you to build a system for them
     * the system needs to handle three major areas of their trade:
     *      1. Production
     *      2. Stock
     *      3. Distribution
     * of the following drugs
     *  Marijuana
     *      Morthern Lights ($70/kg)
     *      Purple Haze ($100/kg)
     *      White Widow ($125/kg)
     *  Cocaine
     *      % different purities (multiply with the value to get final value)
     *          high 90%
     *          medium 75%
     *          low 60%
     *  MDMA
     *      5g, 10g, 15g
     *      Brand (star, alien, joker)
     *
     * For production:
     * Store a collecction of properties these can vary between
     *      1. Rural
     *      2. Jungle
     *      3. Urban
     * and each property has a size
     *      1. Rural in ha (hectare)
     *      2. Jungle in ha (hectare)
     *      3. Urban in m^2 (square meter)
     *
     * Rural properties can manufacture Marijuana
     * Jungle properties can manufacture Cocaine
     * Urban properties can manufacture MDMA
     *
     * Note:
     *  Marijuana has a strain (sativa or indica)
     *  Cocain has a purity (in %)
     *  MDMA has a symbol (joker, alien, star) and a pill weight (in grams)
     *
     * Once all of the properties have been processed and stored
     * the system needs to calculate the production over a given time period
     * the production is dependent on the drug, size of property, as well as the time period
     *
     * Production per area is given by the following:
     *      1ha = 10kg marijuana a month
     *      1ha = 100kg cocaine every 2 weeks
     *      10m^2 = 50 MDMA pills a week
     *
     * Storage:
     *
     * The system needs to take stock, and generate a report of the total value
     * of each of the drugs, as well as a combined value, in two states:
     *  Raw:
     *      mj = $100/kg
     *      coke = $10000/kg
     *      mgma $10/pill
     *  Cut (only Cocaine can be cut):
     *      light $12000/kg
     *      medium $14000/kg
     *      heavy $16000/kg
     *
     * Distribution:
     *
     * The system needs to handle distribution.
     * A King Pin distributes to a Distributor
     * A Distributor distributes to a Street Dealer
     *
     * A King Pin handles transactions above $10000
     * A Distributor handles transactions above $1000 and below $10000
     * A Street Dealer handles transactions below $1000
     *
     * All transactions must first take place through a Street Dealer
     *
     * To fullfil the kata you will need to:
     *  1. Use proper TDD, and create the proper tests
     *  2. Use Abstract Factory, Factory, Strategy and Chain of Command patterns
     */

    internal class Program
    {
        #region Drugs
        abstract class Drugs
        {
            public abstract double Worth { get; set; }
        }

        class Marijuana : Drugs
        {
            public enum Type
            {
                MorthernLights,
                PurpleHaze,
                WhiteWidow
            }

            public override double Worth { get; set; }

            public Marijuana(Type type)
            {
                switch (type)
                {
                    case Type.MorthernLights:
                        Worth = 70;
                        break;
                    case Type.PurpleHaze:
                        Worth = 100;
                        break;
                    case Type.WhiteWidow:
                        Worth = 125;
                        break;
                    default:
                        break;
                }
            }
        }

        class Cocaine : Drugs
        {
            public override double Worth { get; set; }

            public enum Type
            {
                Raw,
                Cut
            }

            public enum Size
            {
                Small,
                Medium,
                Large
            }

            public Cocaine(Type type, Size size, double purity)
            {
                Worth = CalcWorth(type, size, purity);
            }

            public double CalcWorth(Type type, Size size, double purity)
            {
                int worth = 0;
                switch (type)
                {
                    case Type.Raw:
                        worth = 10000;
                        break;
                    case Type.Cut:
                        switch (size)
                        {
                            case Size.Small:
                                worth = 12000;
                                break;
                            case Size.Medium:
                                worth = 14000;
                                break;
                            case Size.Large:
                                worth = 16000;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                return worth * purity / 100;
            }
        }

        class MDMA : Drugs
        {
            public override double Worth { get; set; }

            public MDMA()
            {
                Worth = 10;
            }
        }
        #endregion

        #region Production
        abstract class Production
        {
            public string AreaName { get; set; }
            public abstract int AreaSize { get; set; }
            public abstract Drugs DrugProduction { get; set; }
            public abstract int ProducedInAMonth { get; }
            public abstract double CalcAmount(double timeInMonths);
            public abstract double AmountStored { get; set; }
            public abstract double CalcWorth();
        }

        class Rural : Production
        {
            public Rural(string propertyName, int AreaSize, Marijuana.Type type)
            {
                this.AreaName = propertyName;
                this.DrugProduction = new Marijuana(type);
                this.AreaSize = AreaSize;
            }

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize * timeInMonths;
            }

            public override double CalcWorth() => AmountStored * DrugProduction.Worth;

            public override int AreaSize { get; set; }

            public override Drugs DrugProduction { get; set; }

            public override int ProducedInAMonth => 10;

            public override double AmountStored { get; set; }
        }

        class Jungle : Production
        {
            public Jungle(string propertyName, int AreaSize, Cocaine.Type Type, Cocaine.Size Size, double Purity)
            {
                this.AreaName = propertyName;
                this.AreaSize = AreaSize;
                this.DrugProduction = new Cocaine(Type, Size, Purity);
            }

            public override int AreaSize { get; set; }

            public override Drugs DrugProduction { get; set; }

            public override int ProducedInAMonth => 200;

            public override double AmountStored { get; set; }

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize * timeInMonths;
            }

            public override double CalcWorth()
            {
                return DrugProduction.Worth * AmountStored;
            }
        }

        class Urban : Production
        {
            public Urban(string propertyName, int AreaSize)
            {
                this.AreaName = propertyName;
                this.AreaSize = AreaSize;
                this.DrugProduction = new MDMA();
            }

            public override int AreaSize { get; set; }

            public override Drugs DrugProduction { get; set; }

            public override int ProducedInAMonth => 20;

            public override double AmountStored { get; set; }

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize * timeInMonths;
            }

            public override double CalcWorth()
            {
                return DrugProduction.Worth * AmountStored;
            }
        }

        #endregion

        #region Distribution
        abstract class Distribution
        {
            protected Distribution distributor;

            public void SetChain(Distribution distributor)
            {
                this.distributor = distributor;
            }

            public abstract void ProcessSale(Production production);
        }

        class StreetDealer : Distribution
        {
            public override void ProcessSale(Production production)
            {
                if (production.CalcWorth() < 1000)
                {
                    Console.WriteLine($"Property is worth $ {production.CalcWorth()}, sale will be handled by a street dealer");
                }
                else if (distributor != null)
                {
                    distributor.ProcessSale(production);
                }
            }
        }

        class Distributor : Distribution
        {
            public override void ProcessSale(Production production)
            {
                if (production.CalcWorth() < 10000)
                {
                    Console.WriteLine($"Property is worth $ {production.CalcWorth()}, sale will be handled by a distributor");
                }
                else if (distributor != null)
                {
                    distributor.ProcessSale(production);
                }
            }
        }

        class KingPin : Distribution
        {
            public override void ProcessSale(Production production)
            {
                Console.WriteLine($"Property is worth $ {production.CalcWorth()}, sale will be handled by a king pin");
            }
        }

        #endregion

        private static void Main()
        {
            Distribution streetDealer = new StreetDealer();
            Distribution distributor = new Distributor();
            Distribution kingPin = new KingPin();

            streetDealer.SetChain(distributor);
            distributor.SetChain(kingPin);

            double timeInMonths = 1;
            List<Production> productionSites = new List<Production>();
            productionSites.Add(new Rural("Property 1", 10, Marijuana.Type.MorthernLights));
            productionSites.Add(new Jungle("Property 2", 1, Cocaine.Type.Cut, Cocaine.Size.Large, 50));
            productionSites.Add(new Urban("Property 3", 1));

            foreach (var item in productionSites)
            {
                item.AmountStored = item.CalcAmount(timeInMonths);
                Console.Write(item.AreaName + " creates ");
                Console.Write(item.AmountStored.ToString() + ". ");
                streetDealer.ProcessSale(item);
            }

            Console.ReadLine();
        }
    }
}
