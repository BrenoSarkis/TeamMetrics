using TeamMetrics.Common.Domain;

namespace TeamMetrics.Domain.Boundaries;

public interface Handler<in TMessage> where TMessage : Message {
    Task Handle(TMessage command);
}

public interface Handler<in TMensagem, TResultado> where TMensagem : Message {
    Task<TResultado> Handle(TMensagem mensagem);
}