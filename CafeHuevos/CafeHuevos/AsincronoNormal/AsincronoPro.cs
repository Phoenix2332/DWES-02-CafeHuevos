using Serilog;

namespace CafeHuevos.AsincronoNormal;

public class AsincronoPro {
    private readonly ILogger _logger = Log.ForContext<Sincrono>();

    public async Task PrepararCafe() {
        _logger.Debug("[ASYNC] Preparando café");
        await Task.Delay(200);
        _logger.Debug("[ASYNC] Café listo");
    }

    public async Task CalentarSarten() {
        _logger.Debug("[ASYNC] Calentando la sartén");
        await Task.Delay(200);
        _logger.Debug("[ASYNC] Sartén lista");
    }

    public async Task FreirHuevo() {
        _logger.Debug("[ASYNC] Friendo huevo");
        await Task.Delay(300);
        _logger.Debug("[ASYNC] Huevo listo");
    }

    public async Task FreirBacon() {
        _logger.Debug("[ASYNC] Friendo bacon");
        await Task.Delay(300);
        _logger.Debug("[ASYNC] Bacon listo");
    }

    public async Task TostarPan() {
        _logger.Debug("[ASYNC] Tostando pan");
        await Task.Delay(200);
        _logger.Debug("[ASYNC] Pan listo");
    }

    public async Task UntarMermelada() {
        _logger.Debug("[ASYNC] Untando mermelada");
        await Task.Delay(100);
        _logger.Debug("[ASYNC] Mermelada lista");
    }

    public async Task PrepararZumo() {
        _logger.Debug("[ASYNC] Preparando zumo");
        await Task.Delay(200);
        _logger.Debug("[ASYNC] Zumo listo");
    }
}