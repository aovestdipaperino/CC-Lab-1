using System.Text;
using TaskCli.Model;

namespace TaskCli.Output;

public static class TaskTableFormatter
{
    public static string Format(IReadOnlyList<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            return "No tasks found." + Environment.NewLine;
        }

        var sb = new StringBuilder();
        sb.AppendLine("ID   STATUS  PRIORITY  TAG         TITLE");
        foreach (var t in tasks)
        {
            var state = t.State == TaskState.Done ? "done" : "open";
            var tag = string.IsNullOrEmpty(t.Tag) ? "-" : t.Tag;
            sb.AppendLine($"{t.Id,-4} {state,-7} {t.Priority,-9} {tag,-11} {t.Title}");
        }

        return sb.ToString();
    }
}
