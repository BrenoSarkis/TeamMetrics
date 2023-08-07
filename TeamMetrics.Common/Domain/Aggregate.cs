using System.Collections.Immutable;

namespace TeamMetrics.Common.Domain;

public class Aggregate : Entity {
    private readonly List<Evento> eventos = new();

    public virtual IReadOnlyCollection<Evento> Eventos => eventos.ToImmutableList();

    protected virtual void AdicionarEventos(params Evento[] eventos) {
        eventos.ForEach(AdicionarEvento);
    }

    protected virtual void AdicionarEvento(Evento evento) {
        eventos.Add(evento);
    }

    public virtual void LimparEventos() {
        eventos.Clear();
    }
}