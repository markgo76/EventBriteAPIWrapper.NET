EventBriteAPIWrapper.NET
========================

A concise server-side .NET (C#) wrapper for the core EventBrite API interactions

This set of classes assumes an OAuth2 server-flow set up for managing multiple EventBrite user accounts by proxy.
You will need to hook up your configs to the placeholders for your master account, and set up storage for your users' tokens. This API will launch the Auth process automatically when needed.
When authorised, you can simply populate an object and call Create().
