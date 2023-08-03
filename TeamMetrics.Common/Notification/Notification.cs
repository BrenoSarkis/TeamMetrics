using TeamMetrics.Common.Enumerator;

namespace TeamMetrics.Common.Notification;

public sealed class NotificationType : Enumerator<NotificationType>
{
    public static readonly NotificationType Success = new("success", "Success");
    public static readonly NotificationType Info = new("info", "Info");
    public static readonly NotificationType Alert = new("alert", "Alert");
    public static readonly NotificationType Error = new("error", "Error");
    public static readonly NotificationType Log = new("log", "Log");

    private NotificationType(string id, string name) : base(id, name)
    {
    }

    public static implicit operator string(NotificationType type) => type?.ToString();

    public static implicit operator NotificationType(string @string)
    {
        if (@string is null)
        {
            return null;
        }

        return WithIdOrName(@string, true);
    }
}

public class Notification : IEquatable<Notification>
{
    public static Notification Success(string titulo, string texto)
    {
        return new Notification(NotificationType.Success, titulo, texto);
    }

    public static Notification Success(string texto)
    {
        return new Notification(NotificationType.Success, "", texto);
    }

    public static Notification Info(string titulo, string texto)
    {
        return new Notification(NotificationType.Info, titulo, texto);
    }

    public static Notification Info(string texto)
    {
        return new Notification(NotificationType.Info, "", texto);
    }

    public static Notification Log(string titulo, string texto)
    {
        return new Notification(NotificationType.Log, titulo, texto);
    }

    public static Notification Log(string texto)
    {
        return new Notification(NotificationType.Log, "", texto);
    }

    public static Notification Alert(string titulo, string texto)
    {
        return new Notification(NotificationType.Alert, titulo, texto);
    }

    public static Notification Alert(string texto)
    {
        return new Notification(NotificationType.Alert, "", texto);
    }

    public static Notification Error(string titulo, string texto)
    {
        return new Notification(NotificationType.Error, titulo, texto);
    }

    public static Notification Error(string texto)
    {
        return new Notification(NotificationType.Error, "", texto);
    }

    public static Notification Error(string titulo, Exception erro)
    {
        return new Notification(NotificationType.Error, titulo, erro.Message);
    }

    public static Notification Error(Exception erro)
    {
        return new Notification(NotificationType.Error, "", erro.Message);
    }

    protected Notification(NotificationType type, string title, string text)
    {
        type.ThrowIfNull();
        text.ThrowIfNullOrWhiteSpace();

        Type = type.Name;
        Title = title ?? "";
        Text = text;
    }

    public string Type { get; }
    public string Title { get; }
    public string Text { get; }
    public dynamic Metadata { get; private set; }

    public Notification WithMetadata(dynamic metadata)
    {
        var notification = new Notification(Type, Title, Text)
        {
            Metadata = metadata
        };

        return notification;
    }

    public override bool Equals(object other)
    {
        return Equals(other as Notification);
    }

    public bool Equals(Notification other)
    {
        if (other == null)
        {
            return false;
        }

        return Type == other.Type &&
            Title == other.Title &&
            Text == other.Text;
    }

    public override int GetHashCode()
    {
        return $"{Type.GetHashCode()};{Title.GetHashCode()};{Text.GetHashCode()}".GetHashCode();
    }
}