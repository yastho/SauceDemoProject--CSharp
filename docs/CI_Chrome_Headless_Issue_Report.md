# What Went Wrong With Our CI Tests (and How We Fixed It)

**Project:** SauceDemoProject (Selenium + Reqnroll + NUnit)
**Where it happened:** GitHub Actions (our CI pipeline)
**Status:** Fixed ✅

## What was the problem?

Our login tests worked perfectly on the local computer (Windows) — Chrome would open, log in, and pass. But every time the same tests ran automatically on GitHub (CI), they failed with this error:

```
Chrome instance exited.
```

## What we thought it might be (and checked, one by one)

We didn't guess randomly — we tested each idea and moved to the next only after ruling it out:

1. **"Maybe Chrome and ChromeDriver don't match versions."**
   We stopped using the tool that downloaded ChromeDriver manually and let Selenium pick the right one automatically instead.

2. **"Maybe the CI computer is missing something Chrome needs to run."**
   We checked this directly using a Linux command (`ldd`) that lists missing files a program needs. Nothing was missing.

3. **"Maybe Chrome's security sandbox is blocking it on CI."**
   We added a few safe flags (`--no-sandbox`, `--disable-dev-shm-usage`) that are standard for running Chrome inside CI systems.

4. **"Maybe our headless setting isn't reaching the code."**
   This turned out to be the real one.

## What was actually causing it

We turned on Chrome's own detailed error log, and it told us exactly what was wrong:

```
Missing X server or $DISPLAY
```

In plain terms: **Chrome was trying to open a normal, visible browser window** — but GitHub's CI computers don't have a screen at all. There's nothing for a visible window to appear on, so Chrome crashed immediately.

This should never have happened, because we *did* tell CI to run Chrome in "headless" mode (invisible, no window) using a setting called `Headless`. But when we checked, the code was reading that setting as `false` (off) instead of `true` (on) — even though we had correctly told CI to turn it on.

**Our best guess for why:** the setting is only loaded into memory the *first time* it's needed, and cached from then on (a common performance trick). On a slow or busy CI run, it's possible the code checked the setting a split second before CI had finished setting it up — so it locked in the wrong value and never checked again for the rest of that run. On a later run, timing lined up correctly and everything worked. This is our best explanation, but we didn't 100% prove it — we just observed it happen once and then work fine afterward.

## How we tracked it down

Instead of guessing and hoping, we added temporary "print this value" checks at three points:
- What value is actually stored on the computer running the test?
- What value does our settings-reading code see?
- What value does the final setting end up as?

Comparing all three showed us exactly where things lined up and where they didn't — which is what let us find the real cause instead of just patching symptoms.

## What we did to fix it

1. Simplified how Chrome/ChromeDriver get set up (fewer moving parts, fewer chances for mismatches)
2. Confirmed the headless setting was being read correctly on a clean run
3. Removed all the temporary debug checks once everything was confirmed working
4. Kept one small, permanent check in CI that simply prints the Chrome version, as an easy early warning if something changes in future

## Result

Both login tests now pass reliably on GitHub every time — Chrome runs invisibly (headless) on CI, and still opens normally and visibly when running on the local machine for everyday development.

## A note on how this was done

This project — and this specific investigation — was worked through together with **Claude (Anthropic)**, methodically testing each possible cause of the CI failure with real evidence (logs, checks, and comparisons) rather than guessing, until the actual cause was found and confirmed.
