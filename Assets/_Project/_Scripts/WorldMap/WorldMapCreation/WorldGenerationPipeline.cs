using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps;
using Debug = UnityEngine.Debug;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation
{
    public class WorldGenerationPipeline
    {
        private readonly List<IWorldGenerationStep> _steps = new();

        public void AddStep(IWorldGenerationStep step)
        {
            _steps.Add(step);
        }

        public void RemoveStep(IWorldGenerationStep step)
        {
            _steps.Remove(step);
        }

        public IEnumerator Execute(WorldContext context, WorldCreationParameters settings)
        {
            DateTime totalNow = DateTime.Now;

            HashSet<Type> executedSteps = new();

            foreach (IWorldGenerationStep step in _steps)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                List<Type> dependencies = step.RequiredDependencies;

                if (dependencies == null || dependencies.All(dep => executedSteps.Contains(dep)))
                {
                    yield return step.Process(context, settings);
                    executedSteps.Add(step.GetType());
                }
                else
                    ThrowMissingDependencyException(step);

                stopwatch.Stop();
                Debug.Log($"Process time <b><i>{step.GetType().Name}</i></b>: {stopwatch.ElapsedMilliseconds} ms");
            }

            Debug.Log($"Total world generation duration: {(DateTime.Now - totalNow).TotalMilliseconds:N} ms");
        }

        private static void ThrowMissingDependencyException(IWorldGenerationStep step)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\nCannot execute {step.GetType().Name} as its dependencies have not been executed.");
            sb.AppendLine("This step depends on:");

            foreach (Type dependency in step.RequiredDependencies)
                sb.AppendLine($"- <b><i>{dependency.Name}</i></b>");

            throw new InvalidOperationException(sb.ToString());
        }
    }
}