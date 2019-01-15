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
     *      Morthern Lights
     *      Purple Haze
     *      White Widow
     *  Cocaine
     *      % different purities
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
        abstract class Production
        {
            public abstract string AreaType { get; }
            public abstract int AreaSize { get; set; }
            public abstract string DrugProduction { get; }
            public abstract int ProducedInAMonth { get; }
            public abstract double CalcAmount(double timeInMonths);
        }

        class Rural : Production
        {
            public Rural(int AreaSize)
            {
                this.AreaSize = AreaSize;
            }

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize / timeInMonths;
            }

            public override string AreaType => "Rural";

            public override int AreaSize { get; set; }

            public override string DrugProduction => "Marijuana";

            public override int ProducedInAMonth => 10;
        }

        class Jungle : Production
        {
            public Jungle(int AreaSize)
            {
                this.AreaSize = AreaSize;
            }

            public override string AreaType => "Jungle";

            public override int AreaSize { get; set; }

            public override string DrugProduction => "Cocaine";

            public override int ProducedInAMonth => 200;

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize / timeInMonths;
            }
        }

        class Urban : Production
        {
            public Urban(int AreaSize)
            {
                this.AreaSize = AreaSize;
            }

            public override string AreaType => "Urban";

            public override int AreaSize { get; set; }

            public override string DrugProduction => "MDMA";

            public override int ProducedInAMonth => 20;

            public override double CalcAmount(double timeInMonths)
            {
                return ProducedInAMonth * AreaSize / timeInMonths;
            }
        }

        private static void Main()
        {
            double timeInMonths = 0.5;
            List<Production> productionSites = new List<Production>();
            productionSites.Add(new Rural(10));
            productionSites.Add(new Jungle(10));
            productionSites.Add(new Urban(10));

            foreach (var item in productionSites)
            {
                double test = item.CalcAmount(timeInMonths);
            }
        }
    }
}
