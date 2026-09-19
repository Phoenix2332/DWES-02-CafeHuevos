using System.Diagnostics;
using System.Text;
using CafeHuevos;
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
    WriteLine("Proceso terminado");
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
    WriteLine("Proceso terminado");
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
    WriteLine("Proceso terminado");
    WriteLine($"Tiempo total: {cronometro.ElapsedMilliseconds} milisegundos.");
    var estadoCafe = cronometro.ElapsedMilliseconds > 500 ? CafeFrio : CafeCaliente;
    WriteLine(estadoCafe);
    WriteLine();
}