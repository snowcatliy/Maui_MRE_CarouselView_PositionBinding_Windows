This repository serves as a Minimal Reproducible Example (MRE) for a reported bug in .NET MAUI's `CarouselView` regarding unexpected behavior with `TwoWay` binding on the `Position` property, specifically on the Windows platform.

This MRE is linked to GitHub Issue [#29215](https://github.com/dotnet/maui/issues/29215) where the problem is reported and discussed in detail.

## Problem Description

When using `TwoWay` binding on the `CarouselView.Position` property and updating the bound ViewModel property programmatically (e.g., by clicking a button), the CarouselView on **Windows** fails to navigate correctly. Debug logs show unexpected back-and-forth updates to the ViewModel property and CarouselView Position during the visual transition, causing the view to remain stuck on the previous page or exhibit erratic behavior. On **Android**, the visual navigation works correctly despite similar multiple binding updates occurring in the background.

## Steps to Reproduce

1.  Clone this repository to your local machine.
2.  Open the solution in Visual Studio.
3.  Build and run the project on the **Windows Machine (.NET 9)** target.
4.  If the CarouselView is not currently on Page 2, click the "Page 2" button.
5.  Click the "Page 1" button.
6.  Observe the visual behavior of the `CarouselView` (it should fail to navigate to Page 1 or jump back and forth) and check the output console for debug logs.
7.  (Optional) Build and run the project on an **Android Emulator/Device (.NET 9)**. Repeat step 5. Observe that the visual navigation works correctly on Android despite similar log output indicating multiple binding updates.

## Expected Behavior

Clicking a button bound to update the ViewModel's `CurrentTabIndex` property should cause the `CarouselView` to smoothly animate and visually switch to the corresponding page (e.g., from Page 2 to Page 1). The `PositionChanged` event and `TwoWay` binding should ideally only report the final target position consistently.

## Actual Behavior

Clicking a button to set `CurrentTabIndex` to 0 (when currently at 1) on Windows results in the CarouselView failing to visually navigate and debug logs showing `ViewModel.CurrentTabIndex` and `CarouselView.Position` rapidly updating between 0 and 1 before settling back at 1.

## Visual Evidence

**Windows Behavior GIF:**

![Windows Behavior](windows.webp)

**Android Behavior GIF:**

![Android Behavior](android.webp)

## Relevant Files

-   MainPage.xaml
-   MainPage.xaml.cs

## Environment

-   .NET Version: 9.0.100
-   .NET MAUI Version: 9.0.14
-   OS: Windows 10 22H2
-   IDE: Visual Studio 2022 17.13.6

---

**Related GitHub Issue:**

This MRE is the reproduction project for Issue [#29215](https://github.com/dotnet/maui/issues/29215) in the dotnet/maui repository. Please refer to that issue for detailed discussion, logs, and visual evidence (GIFs).

*(Note: This issue might be related to [#29216](https://github.com/dotnet/maui/issues/29216), which reports a distinct problem where `IsSwipeEnabled="False"` prevents visual navigation on Windows.)*
