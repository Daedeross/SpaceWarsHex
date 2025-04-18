﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance", Justification = "Testing should be through interfaces members to ensure class implements properly.", Scope = "module")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Test Data", Scope = "module")]
