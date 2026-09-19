using System.Diagnostics;
using System.Text;
using CafeHuevos;
using CafeHuevos.AsincronoCancellation;
using CafeHuevos.AsincronoNormal;
using Serilog;
using static System.Console;

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .Enrich.WithProperty("Application", "CafeHuevos")
    .CreateLogger();
Log.Logger = loggerConfiguration;

OutputEncoding = Encoding.UTF8;

var sincrono = new Sincrono();
var asincronoMal = new AsincronoMal();
var asincronoPro = new AsincronoPro();
var asincronoMalCancellation = new AsincronoMalCancellation();
var asincronoProCancellation = new AsincronoProCancellation();
var cronometro = new Stopwatch();
const string CafeFrio = "El cafe se ha enfriado, no puedo tomarlo.";
const string CafeCaliente = "El cafe aun esta caliente, puedo tomarlo.";

await Main();
WriteLine("👋 Presiona una tecla para salir...");
ReadKey();
return;

async Task Main() {
    MostrarSincrono();
    await MostrarAsincronoMal();
    await MostrarAsincronoPro();
    await MostrarAsincronoMalCancellation();
    await MostrarAsincronoProCancellation();
}

void MostrarSincrono() {
    Log.Debug("[PROGRAM] Iniciando programa Síncrono");
    cronometro.Restart();
    sincrono.PrepararCafe();
    sincrono.CalentarSarten();
    sincrono.FreirHuevo();
    sincrono.FreirBacon();
    sincrono.TostarPan();
    sincrono.UntarMermelada();
    sincrono.PrepararZumo();
    cronometro.Stop();
    WriteLine("Proceso Síncrono terminado");
    WriteLine($"Tiempo total: {cronometro.ElapsedMilliseconds} milisegundos.");
    var estadoCafe = cronometro.ElapsedMilliseconds > 500 ? CafeFrio : CafeCaliente;
    WriteLine(estadoCafe);
    WriteLine();
}

async Task MostrarAsincronoMal() {
    Log.Debug("[PROGRAM] Iniciando programa Asíncrono Mal");
    cronometro.Restart();
    await asincronoMal.PrepararCafe();
    await asincronoMal.CalentarSarten();
    await asincronoMal.FreirHuevo();
    await asincronoMal.FreirBacon();
    await asincronoMal.TostarPan();
    await asincronoMal.UntarMermelada();
    await asincronoMal.PrepararZumo();
    cronometro.Stop();
    WriteLine("Proceso AsíncronoMal terminado");
    WriteLine($"Tiempo total: {cronometro.ElapsedMilliseconds} milisegundos.");
    var estadoCafe = cronometro.ElapsedMilliseconds > 500 ? CafeFrio : CafeCaliente;
    WriteLine(estadoCafe);
    WriteLine();
}

async Task MostrarAsincronoPro() {
    Log.Debug("[PROGRAM] Iniciando programa Asíncrono Pro");
    cronometro.Restart();
    var pan = asincronoPro.TostarPan().ContinueWith(async _ => await asincronoPro.UntarMermelada()).Unwrap();
    var sarten = asincronoPro.CalentarSarten().ContinueWith(async _ =>
        await Task.WhenAll(asincronoPro.FreirHuevo(), asincronoPro.FreirBacon())).Unwrap();
    await Task.WhenAll(asincronoPro.PrepararCafe(), pan, sarten, asincronoPro.PrepararZumo());
    /*await Task.WhenAll(asincronoPro.PrepararCafe(), asincronoPro.CalentarSarten(), asincronoPro.PrepararZumo());
    await Task.WhenAll(asincronoPro.FreirHuevo(), asincronoPro.FreirBacon(), asincronoPro.UntarMermelada());*/
    cronometro.Stop();
    WriteLine("Proceso AsíncronoPro terminado");
    WriteLine($"Tiempo total: {cronometro.ElapsedMilliseconds} milisegundos.");
    var estadoCafe = cronometro.ElapsedMilliseconds > 500 ? CafeFrio : CafeCaliente;
    WriteLine(estadoCafe);
    WriteLine();
}

async Task MostrarAsincronoMalCancellation() {
    Log.Debug("[PROGRAM] Iniciando Asíncrono Mal (Cancelación)");
    using var cts = new CancellationTokenSource(500);
    cronometro.Restart();
    try {
        await asincronoMalCancellation.PrepararCafe(cts.Token);
        await asincronoMalCancellation.CalentarSarten(cts.Token);
        await asincronoMalCancellation.FreirHuevo(cts.Token);
        await asincronoMalCancellation.FreirBacon(cts.Token);
        await asincronoMalCancellation.TostarPan(cts.Token);
        await asincronoMalCancellation.UntarMermelada(cts.Token);
        await asincronoMalCancellation.PrepararZumo(cts.Token);
    }
    catch (OperationCanceledException) {
        WriteLine("⛔ Proceso cancelado (500 ms)");
    }

    cronometro.Stop();
    WriteLine("Proceso AsíncronoMalCancellation terminado");
    WriteLine($"Tiempo: {cronometro.ElapsedMilliseconds} ms");
    WriteLine();
}

async Task MostrarAsincronoProCancellation() {
    Log.Debug("[PROGRAM] Iniciando Asíncrono Pro (Cancelación)");
    using var cts = new CancellationTokenSource(500);
    cronometro.Restart();
    try {
        var pan = asincronoProCancellation.TostarPan(cts.Token)
            .ContinueWith(_ => asincronoProCancellation.UntarMermelada(cts.Token)).Unwrap();
        var sarten = asincronoProCancellation.CalentarSarten(cts.Token)
            .ContinueWith(_ => Task.WhenAll(asincronoProCancellation.FreirHuevo(cts.Token),
                asincronoProCancellation.FreirBacon(cts.Token))).Unwrap();
        await Task.WhenAll(asincronoProCancellation.PrepararCafe(cts.Token), pan, sarten,
            asincronoProCancellation.PrepararZumo(cts.Token));
    }
    catch (OperationCanceledException) {
        WriteLine("⛔ Proceso cancelado (500 ms)");
    }

    cronometro.Stop();
    WriteLine("Proceso AsíncronoProCancellation terminado");
    WriteLine($"Tiempo: {cronometro.ElapsedMilliseconds} ms");
    WriteLine();
}