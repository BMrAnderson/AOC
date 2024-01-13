using Core.CubeConundrum;
using Core.CubeConundrum.Implementation;
using Core.GearRatios;
using Core.GearRatios.Implementation;
using Core.Implementation;
using Core.Scratchcards;
using Core.Scratchcards.Implementation;
using Core.Trebuchet;
using Core.Trebuchet.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IDocumentLinesReaderAsync, DocumentLinesReaderAsync>();
            services.AddTransient<ICalibrationLinesDigitsResolver, CalibrationLinesDigitsResolver>();
            services.AddTransient<ICalibrationLineParser, CalibrationLineDigitsParser>();

            services.AddTransient<ISubsetFactory, SubsetFactory>();
            services.AddTransient<ISubsetsLineParser, SubsetsLineParser>();
            services.AddTransient<IGameFactory, GameFactory>();
            services.AddTransient<IGameLineParser, GameLineParser>();
            services.AddTransient<ICubeConundrum, CubeConundrum.Implementation.CubeConundrum>();

            services.AddTransient<IEngineSchematicParserAsync, EngineSchematicParserAsync>();
            services.AddTransient<IEngineSchematic, EngineSchematic>();

            services.AddTransient<ICardLineParser, CardLineParser>();
            services.AddTransient<IScratchCards, ScratchCards>();

            return services;
        }
    }
}
