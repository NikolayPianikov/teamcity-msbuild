namespace JetBrains.TeamCity.MSBuildLogger
{
	using System;

	using Microsoft.Build.Framework;

	public class Logger : ILogger
	{
		public string Parameters { get; set; }

		public LoggerVerbosity Verbosity { get; set; }

		public void Initialize(IEventSource eventSource)
		{
			eventSource.BuildStarted += EventSourceOnBuildStarted;
			eventSource.BuildFinished += EventSourceOnBuildFinished;
			eventSource.ProjectStarted += EventSourceOnProjectStarted;
			eventSource.ProjectFinished += EventSourceOnProjectFinished;
			eventSource.TargetStarted += EventSourceOnTargetStarted;
			eventSource.TargetFinished += EventSourceOnTargetFinished;
			eventSource.MessageRaised += EventSourceOnMessageRaised;
		}		

		public void Shutdown()
		{			
		}

		private static void EventSourceOnBuildStarted(object sender, BuildStartedEventArgs buildStartedEventArgs)
		{
			Console.WriteLine(buildStartedEventArgs.Message);
		}

		private static void EventSourceOnBuildFinished(object sender, BuildFinishedEventArgs buildFinishedEventArgs)
		{
			Console.WriteLine(buildFinishedEventArgs.Message);
		}

		private static void EventSourceOnProjectStarted(object sender, ProjectStartedEventArgs projectStartedEventArgs)
		{
			Console.WriteLine(projectStartedEventArgs.Message);
		}

		private static void EventSourceOnProjectFinished(object sender, ProjectFinishedEventArgs projectFinishedEventArgs)
		{			
			Console.WriteLine(projectFinishedEventArgs.Message);
		}

		private static void EventSourceOnTargetStarted(object sender, TargetStartedEventArgs targetStartedEventArgs)
		{
			Console.WriteLine(targetStartedEventArgs.Message);
		}

		private static void EventSourceOnTargetFinished(object sender, TargetFinishedEventArgs targetFinishedEventArgs)
		{
			Console.WriteLine(targetFinishedEventArgs.Message);
		}

		private void EventSourceOnMessageRaised(object sender, BuildMessageEventArgs buildMessageEventArgs)
		{
			if (ShouldBeLogged(buildMessageEventArgs.Importance))
			{				
				Console.WriteLine(buildMessageEventArgs.Message);
			}
		}

		private bool ShouldBeLogged(MessageImportance importance)
		{
			switch (Verbosity)
			{
				case LoggerVerbosity.Quiet:
					return false;

				case LoggerVerbosity.Minimal:
					return importance == MessageImportance.High;

				case LoggerVerbosity.Normal:
					return importance == MessageImportance.High || importance == MessageImportance.Normal;

				case LoggerVerbosity.Detailed:
				case LoggerVerbosity.Diagnostic:
					return true;

				default:
					throw new NotSupportedException(importance.ToString());
			}
		}
	}
}
