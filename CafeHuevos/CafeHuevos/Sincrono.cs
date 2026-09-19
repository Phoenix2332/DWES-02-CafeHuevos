using Serilog;

namespace CafeHuevos;

public class Sincrono {
    private readonly ILogger _logger = Log.ForContext<Sincrono>();

    public void PrepararCafe() {
        _logger.Debug("[SYNC] Preparando café");
        Thread.Sleep(200);
    }

    public void CalentarSarten() {
        _logger.Debug("[SYNC] Calentando la sartén");
        Thread.Sleep(200);
    }

    public void FreirHuevo() {
        _logger.Debug("[SYNC] Friendo huevo");
        Thread.Sleep(300);
    }

    public void FreirBacon() {
        _logger.Debug("[SYNC] Friendo bacon");
        Thread.Sleep(300);
    }

    public void TostarPan() {
        _logger.Debug("[SYNC] Tostando pan");
        Thread.Sleep(200);
    }

    public void UntarMermelada() {
        _logger.Debug("[SYNC] Untando mermelada");
        Thread.Sleep(100);
    }

    public void PrepararZumo() {
        _logger.Debug("[SYNC] Preparando zumo");
        Thread.Sleep(200);
    }
}