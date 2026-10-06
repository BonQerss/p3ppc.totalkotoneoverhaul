using System.Collections.Generic;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X64;
using Reloaded.Hooks.ReloadedII.Interfaces;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;
using p3ppc.totalkotoneoverhaul.Configuration;
using static p3ppc.totalkotoneoverhaul.Utils;

namespace p3ppc.totalkotoneoverhaul;

internal class ColorLogger
{
    private readonly IReloadedHooks? _hooks;
    private Config _config;
    private readonly HashSet<string> _seen = new();

    private IAsmHook? _renderUiSpriteHook;
    private IAsmHook? _renderSprTextureHook;
    private IAsmHook? _renderTextLowHook;

    private IReverseWrapper<RenderUiSpriteLogDelegate>? _renderUiSpriteWrapper;
    private IReverseWrapper<RenderSprTextureLogDelegate>? _renderSprTextureWrapper;
    private IReverseWrapper<RenderTextLogDelegate>? _renderTextWrapper;

    internal ColorLogger(IReloadedHooks? hooks, Config config)
    {
        _hooks = hooks;
        _config = config;

        if (_hooks == null || !_config.ExperimentalColorLogging)
            return;

        SigScan("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 30 48 8B F1 0F 29 74 24 20 48 8D 0D ?? ?? ?? ??", "Color Logger RenderUISprite", address =>
        {
            string[] function =
            {
                "use64",
                "push r10",
                "push r11",
                "mov r10, [rsp+0x10]",
                "lea r11, [rsp+0x10]",
                "push rax",
                "push rcx",
                "push rdx",
                "push r8",
                "push r9",
                "xor r9d, r9d",
                "movzx eax, byte [r11+0x30]",
                "shl eax, 24",
                "or r9d, eax",
                "movzx eax, byte [r11+0x38]",
                "shl eax, 16",
                "or r9d, eax",
                "movzx eax, byte [r11+0x40]",
                "shl eax, 8",
                "or r9d, eax",
                "movzx eax, byte [r11+0x48]",
                "or r9d, eax",
                "mov rcx, r10",
                "sub rsp, 0x80",
                "movdqu [rsp+0x20], xmm0",
                "movdqu [rsp+0x30], xmm1",
                "movdqu [rsp+0x40], xmm2",
                "movdqu [rsp+0x50], xmm3",
                "movdqu [rsp+0x60], xmm4",
                "movdqu [rsp+0x70], xmm5",
                $"{_hooks.Utilities.GetAbsoluteCallMnemonics(LogRenderUiSprite, out _renderUiSpriteWrapper)}",
                "movdqu xmm0, [rsp+0x20]",
                "movdqu xmm1, [rsp+0x30]",
                "movdqu xmm2, [rsp+0x40]",
                "movdqu xmm3, [rsp+0x50]",
                "movdqu xmm4, [rsp+0x60]",
                "movdqu xmm5, [rsp+0x70]",
                "add rsp, 0x80",
                "pop r9",
                "pop r8",
                "pop rdx",
                "pop rcx",
                "pop rax",
                "pop r11",
                "pop r10"
            };

            _renderUiSpriteHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.ExecuteFirst).Activate();
        });

        SigScan("48 89 5C 24 ?? 57 48 81 EC 80 00 00 00 0F 29 74 24 ?? 0F 29 7C 24 ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B D9", "Color Logger RenderSprTexture", address =>
        {
            string[] function =
            {
                "use64",
                "push r10",
                "push r11",
                "mov r10, [rsp+0x10]",
                "lea r11, [rsp+0x10]",
                "push rax",
                "push rcx",
                "push rdx",
                "push r8",
                "push r9",
                "xor r8d, r8d",
                "movzx eax, byte [r11+0x30]",
                "shl eax, 24",
                "or r8d, eax",
                "movzx eax, byte [r11+0x38]",
                "shl eax, 16",
                "or r8d, eax",
                "movzx eax, byte [r11+0x40]",
                "shl eax, 8",
                "or r8d, eax",
                "movzx eax, byte [r11+0x48]",
                "or r8d, eax",
                "mov rcx, r10",
                "sub rsp, 0x80",
                "movdqu [rsp+0x20], xmm0",
                "movdqu [rsp+0x30], xmm1",
                "movdqu [rsp+0x40], xmm2",
                "movdqu [rsp+0x50], xmm3",
                "movdqu [rsp+0x60], xmm4",
                "movdqu [rsp+0x70], xmm5",
                $"{_hooks.Utilities.GetAbsoluteCallMnemonics(LogRenderSprTexture, out _renderSprTextureWrapper)}",
                "movdqu xmm0, [rsp+0x20]",
                "movdqu xmm1, [rsp+0x30]",
                "movdqu xmm2, [rsp+0x40]",
                "movdqu xmm3, [rsp+0x50]",
                "movdqu xmm4, [rsp+0x60]",
                "movdqu xmm5, [rsp+0x70]",
                "add rsp, 0x80",
                "pop r9",
                "pop r8",
                "pop rdx",
                "pop rcx",
                "pop rax",
                "pop r11",
                "pop r10"
            };

            _renderSprTextureHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.ExecuteFirst).Activate();
        });

        SigScan("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 70 0F 29 74 24 60 48 8D 0D ?? ?? ?? ?? 0F 29 7C 24 50 41 8B E9", "Color Logger RenderTextLow", address =>
        {
            string[] function =
            {
                "use64",
                "push r10",
                "push r11",
                "mov r10, [rsp+0x10]",
                "push rax",
                "push rcx",
                "push rdx",
                "push r8",
                "push r9",
                "mov edx, r9d",
                "mov rcx, r10",
                "sub rsp, 0x80",
                "movdqu [rsp+0x20], xmm0",
                "movdqu [rsp+0x30], xmm1",
                "movdqu [rsp+0x40], xmm2",
                "movdqu [rsp+0x50], xmm3",
                "movdqu [rsp+0x60], xmm4",
                "movdqu [rsp+0x70], xmm5",
                $"{_hooks.Utilities.GetAbsoluteCallMnemonics(LogRenderText, out _renderTextWrapper)}",
                "movdqu xmm0, [rsp+0x20]",
                "movdqu xmm1, [rsp+0x30]",
                "movdqu xmm2, [rsp+0x40]",
                "movdqu xmm3, [rsp+0x50]",
                "movdqu xmm4, [rsp+0x60]",
                "movdqu xmm5, [rsp+0x70]",
                "add rsp, 0x80",
                "pop r9",
                "pop r8",
                "pop rdx",
                "pop rcx",
                "pop rax",
                "pop r11",
                "pop r10"
            };

            _renderTextLowHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.ExecuteFirst).Activate();
        });
    }

    internal void UpdateConfig(Config config)
    {
        _config = config;
    }

    private void LogRenderUiSprite(nuint caller, int bank, int key, uint rgba)
    {
        LogColor("RenderUISprite", caller, rgba, $"bank 0x{bank:X} key 0x{key:X}");
    }

    private void LogRenderSprTexture(nuint caller, int key, uint rgba)
    {
        LogColor("RenderSprTexture", caller, rgba, $"key 0x{key:X}");
    }


    private void LogRenderText(nuint caller, uint rgba)
    {
        LogColor("RenderTextLow", caller, rgba, "");
    }

    private void LogColor(string source, nuint caller, uint rgba, string details)
    {
        if (!_config.ExperimentalColorLogging)
            return;

        byte r = (byte)(rgba >> 24);
        byte g = (byte)(rgba >> 16);
        byte b = (byte)(rgba >> 8);
        byte a = (byte)rgba;

        string key = $"{source}:{caller:X}:{r:X2}{g:X2}{b:X2}:{details}";
        if (!_seen.Add(key))
            return;

        nuint call = caller >= 5 ? caller - 5 : caller;
        string suffix = string.IsNullOrWhiteSpace(details) ? "" : $" {details}";

        Log($"Color {source} call 0x{call:X} return 0x{caller:X} #{r:X2}{g:X2}{b:X2} A{a:X2}{suffix}");
    }

    [Function(CallingConventions.Microsoft)]
    private delegate void RenderUiSpriteLogDelegate(nuint caller, int bank, int key, uint rgba);

    [Function(CallingConventions.Microsoft)]
    private delegate void RenderSprTextureLogDelegate(nuint caller, int key, uint rgba);


    [Function(CallingConventions.Microsoft)]
    private delegate void RenderTextLogDelegate(nuint caller, uint rgba);
}
