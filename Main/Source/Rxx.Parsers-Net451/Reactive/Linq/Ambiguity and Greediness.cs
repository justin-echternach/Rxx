﻿using System;
using System.Diagnostics.Contracts;
using System.Reactive;
using System.Reactive.Linq;

namespace Rxx.Parsers.Reactive.Linq
{
  public static partial class ObservableParser
  {
    /// <summary>
    /// Converts greedy matches from the specified <paramref name="parser"/> into matches that 
    /// have a length of zero.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The greedy parser to be made into a non-greedy parser.</param>
    /// <returns>A parser that converts the greedy matches from the specified <paramref name="parser"/> into 
    /// matches that have a length of zero.</returns>
    public static IObservableParser<TSource, TResult> NonGreedy<TSource, TResult>(
      this IObservableParser<TSource, TResult> parser)
    {
      Contract.Requires(parser != null);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, TResult>>() != null);

      return parser.Yield("NonGreedy", source => parser.Parse(source).Select(result => result.Yield(length: 0)));
    }

    /// <summary>
    /// Parses multiple sequences with the specified <paramref name="parser"/>, starting from the beginning 
    /// of the source sequence and then skipping one element at a time, until there are no matches or the 
    /// source sequence ends.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The unambiguous parser that will parse each consecutive sequence until there are
    /// no matches or the source sequence ends.</param>
    /// <returns>A parser that yields the matches from the specified <paramref name="parser"/> for each 
    /// consecutive sequence, starting from the beginning of the source sequence and then skipping one element 
    /// at a time, until there are no matches or the source sequence ends.</returns>
    public static IObservableParser<TSource, IObservable<TResult>> Ambiguous<TSource, TResult>(
      this IObservableParser<TSource, TResult> parser)
    {
      Contract.Requires(parser != null);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, IObservable<TResult>>>() != null);

      return new AmbiguousObservableParser<TSource, Unit, TResult>(parser);
    }

    /// <summary>
    /// Parses multiple sequences with the specified <paramref name="parser"/>, starting from the beginning 
    /// of the source sequence and then skipping one element at a time, until the specified count is reached
    /// or the source sequence ends.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The unambiguous parser that will parse each consecutive sequence until the specified 
    /// count is reached or the source sequence ends.</param>
    /// <param name="untilCount">Indicates the maximum number of ambiguous matches to find.</param>
    /// <returns>A parser that yields the matches from the specified <paramref name="parser"/> for each 
    /// consecutive sequence, starting from the beginning of the source sequence and then skipping one element 
    /// at a time, until the specified count is reached or the source sequence ends.</returns>
    public static IObservableParser<TSource, IObservable<TResult>> Ambiguous<TSource, TResult>(
      this IObservableParser<TSource, TResult> parser,
      int untilCount)
    {
      Contract.Requires(parser != null);
      Contract.Requires(untilCount >= 0);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, IObservable<TResult>>>() != null);

      return new AmbiguousObservableParser<TSource, Unit, TResult>(parser, untilCount);
    }

    /// <summary>
    /// Parses multiple sequences with the specified <paramref name="parser"/>, starting from the beginning 
    /// of the source sequence and then skipping one element at a time, until the other parser matches or the 
    /// source sequence ends.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TUntilResult">The type of the elements that are generated by the until parser.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The unambiguous parser that will parse each consecutive sequence until the other 
    /// parser matches or the source sequence ends.</param>
    /// <param name="untilParser">The parser that ends the ambiguity when it matches.</param>
    /// <returns>A parser that yields the matches from the specified <paramref name="parser"/> for each 
    /// consecutive sequence, starting from the beginning of the source sequence and then skipping one element 
    /// at a time, until the other parser matches or the source sequence ends.</returns>
    public static IObservableParser<TSource, IObservable<TResult>> Ambiguous<TSource, TUntilResult, TResult>(
      this IObservableParser<TSource, TResult> parser,
      IObservableParser<TSource, TUntilResult> untilParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(untilParser != null);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, IObservable<TResult>>>() != null);

      return new AmbiguousObservableParser<TSource, TUntilResult, TResult>(parser, untilParser);
    }

    /// <summary>
    /// Projects each match from the specified <paramref name="parser"/> into a singleton observable sequence
    /// that contains the match's value.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser from which matches will be projected into singleton observable sequences.</param>
    /// <returns>A parser that yields matches from the specified <paramref name="parser"/> projected into singleton
    /// observable sequences.</returns>
    public static IObservableParser<TSource, IObservable<TResult>> Amplify<TSource, TResult>(
      this IObservableParser<TSource, TResult> parser)
    {
      Contract.Requires(parser != null);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, IObservable<TResult>>>() != null);

      return parser.Yield("Amplify", source => parser.Parse(source).Select(result => result.YieldMany()));
    }

    /// <summary>
    /// Matches the single element from the ambiguous result sequence in each match that is yielded by the specified 
    /// <paramref name="parser"/> and fails for any match in which there is zero or more than one element.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements of the result sequences that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser from which the single result element is yielded for each match.</param>
    /// <returns>A parser that matches the single element from the result sequence of each match that is 
    /// yielded by the specified <paramref name="parser"/> and fails for any match in which there is zero 
    /// or more than one element.</returns>
    public static IObservableParser<TSource, TResult> Single<TSource, TResult>(
      this IObservableParser<TSource, IObservable<TResult>> parser)
    {
      Contract.Requires(parser != null);
      Contract.Ensures(Contract.Result<IObservableParser<TSource, TResult>>() != null);

      return parser.Yield<TSource, IObservable<TResult>, TResult>(
        "Single",
        source => parser.Parse(source).SelectMany(
          result => Observable.Create<IParseResult<TResult>>(
          observer =>
          {
            bool hasResult = false;
            TResult firstResult = default(TResult);

            return result.Value.Take(2).SubscribeSafe(
              innerResult =>
              {
                if (!hasResult)
                {
                  firstResult = innerResult;
                  hasResult = true;
                }
                else
                {
                  hasResult = false;
                }
              },
              observer.OnError,
              () =>
              {
                if (hasResult)
                {
                  observer.OnNext(result.Yield(firstResult));
                }

                observer.OnCompleted();
              });
          })));
    }
  }
}