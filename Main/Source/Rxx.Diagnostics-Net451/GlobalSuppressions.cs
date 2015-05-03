﻿// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "System.Reactive.Linq.Observable2", Justification = "Provides static factory and extension methods.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`1(System.IObservable`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`1(System.IObservable`1<!!0>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`1(System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<!!0>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>,System.Func`2<System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!1>>>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<!!0>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>,System.Func`2<System.IObservable`1<System.Reactive.CollectionNotification`1<!!0>>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!1>>>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<!!1>,System.Func`2<!!1,!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<!!1>,System.Func`2<!!1,!!0>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<System.Reactive.CollectionNotification`1<System.Collections.Generic.KeyValuePair`2<!!0,!!1>>>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`2(System.IObservable`1<System.Reactive.CollectionNotification`1<System.Collections.Generic.KeyValuePair`2<!!0,!!1>>>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`3(System.IObservable`1<!!0>,System.Func`2<!!0,!!1>,System.IObservable`1<System.Reactive.CollectionNotification`1<System.Collections.Generic.KeyValuePair`2<!!1,!!0>>>,System.Func`2<System.IObservable`1<System.Reactive.CollectionNotification`1<System.Collections.Generic.KeyValuePair`2<!!1,!!0>>>,System.IObservable`1<System.Reactive.CollectionNotification`1<System.Collections.Generic.KeyValuePair`2<!!1,!!2>>>>,System.Collections.Generic.IEqualityComparer`1<!!1>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Reactive.Linq.Observable2.#Collect`5(System.Func`2<System.IDisposable,!!0>,System.IObservable`1<!!1>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!3>>,System.Func`2<!!1,!!2>,System.Func`2<!!3,!!2>,System.Func`3<!!2,!!1,System.Reactive.CollectionNotification`1<!!3>>,System.Func`2<System.IObservable`1<System.Reactive.CollectionNotification`1<!!3>>,System.IObservable`1<System.Reactive.CollectionNotification`1<!!4>>>,System.Collections.Generic.IEqualityComparer`1<!!2>)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Rxx.Parsers.ParseResult.#CreateLookAhead`1(!!0,System.Int32)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Rxx.Parsers.ParseResult.#Yield`2(Rxx.Parsers.IParseResult`1<!!0>,!!1,System.Int32)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Rxx.Parsers.ParseResult.#YieldMany`1(Rxx.Parsers.IParseResult`1<!!0>,!!0,System.Int32)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Rxx.Parsers.Reactive.Linq.ObservableParser.#AtLeast`3(Rxx.Parsers.Reactive.IObservableParser`2<!!0,!!2>,System.String,System.Int32,System.Int32,Rxx.Parsers.Reactive.IObservableParser`2<!!0,!!1>,System.Boolean)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Net.Sockets.SocketExtensions.#ReceiveUntilCompleted(System.Net.Sockets.Socket,System.Net.Sockets.SocketFlags)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Net.Sockets.SocketExtensions.#SendToUntilCompleted(System.Net.Sockets.Socket,System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Net.Sockets.SocketExtensions.#SendUntilCompleted(System.Net.Sockets.Socket,System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Net.Sockets.SocketExtensions.#SendToUntilCompleted(System.Net.Sockets.Socket,System.Byte[],System.Int32,System.Int32,System.Net.EndPoint)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "System.Net.Sockets.SocketExtensions.#SendToUntilCompleted(System.Net.Sockets.Socket,System.Byte[],System.Int32,System.Int32,System.Net.EndPoint)", Justification = "Disposable is composited either by the caller or the infrastructure.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "value", Scope = "member", Target = "System.Scalar`1.#System.Collections.Generic.IList`1<!0>.Item[System.Int32]", Justification = "The setter throws.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "comparer", Scope = "member", Target = "System.Scalar`1.#System.Collections.IStructuralEquatable.Equals(System.Object,System.Collections.IEqualityComparer)", Justification = "Parameter name is defined by the interface and the field name is succinct.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "comparer", Scope = "member", Target = "System.Scalar`1.#System.Collections.IStructuralEquatable.GetHashCode(System.Collections.IEqualityComparer)", Justification = "Parameter name is defined by the interface and the field name is succinct.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Rxx.Parsers.Linq", Justification = "Defines a type with many extension methods.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.ComponentModel", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Collections.Generic", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Rxx.Parsers.Reactive.Linq", Justification = "Defines a type with many extension methods.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Data.SqlClient", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Net.NetworkInformation", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.ServiceModel.Syndication", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Windows.Reactive", Justification = "Categorizes Rxx-specific features for WPF.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Windows.Input", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Windows", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.ServiceModel.Reactive", Justification = "Categorizes Rxx-specific features for WCF.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Reactive.Linq", Justification = "Rx namespace with additional types defined in Rx assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Reactive.Disposables", Justification = "Rx namespace with additional types defined in Rx assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Net.Mail", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Linq.Expressions", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Linq", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Rxx", Justification = "Simplifies the discovery of orthogonal Rxx types.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.IO", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Net.Sockets", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Net", Justification = "System namespace with additional types defined in system assemblies.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Microsoft.Phone.UserData", Justification = "Extensions.")]
#if WINDOWS_PHONE
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "False positive.  Windows Phone projects don't support signing.")]
#endif
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System", Justification = "FCL namespace.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Reactive", Justification = "Rx namespace.")]
