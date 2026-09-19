using Serilog;

namespace CafeHuevos.AsincronoCancellation;

public class AsincronoMalCancellation {
    private readonly ILogger _logger = Log.ForContext<Sincrono>();

    public async Task PrepararCafe(CancellationToken ct) {
        _logger.Debug("[ASYNC] Preparando café");
        await Task.Delay(200, ct);
    }

    public async Task CalentarSarten(CancellationToken ct) {
        _logger.Debug("[ASYNC] Calentando la sartén");
        await Task.Delay(200, ct);
    }

    public async Task FreirHuevo(CancellationToken ct) {
        _logger.Debug("[ASYNC] Friendo huevo");
        await Task.Delay(300, ct);
    }

    public async Task FreirBacon(CancellationToken ct) {
        _logger.Debug("[ASYNC] Friendo bacon");
        await Task.Delay(300, ct);
    }

    public async Task TostarPan(CancellationToken ct) {
        _logger.Debug("[ASYNC] Tostando pan");
        await Task.Delay(200, ct);
    }

    public async Task UntarMermelada(CancellationToken ct) {
        _logger.Debug("[ASYNC] Untando mermelada");
        await Task.Delay(100, ct);
    }

    public async Task PrepararZumo(CancellationToken ct) {
        _logger.Debug("[ASYNC] Preparando zumo");
        await Task.Delay(200, ct);
    }
}