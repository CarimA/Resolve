# Resolve

Resolve is a collision detection and resolution library designed for use with *convex polygons*.

This library does not provide a fully featured physics engine but is still very useful for 2D game physics in platformers, top-down games, etc. This library makes use of the Separating Axis Theorem to do the job.

In terms of actual collision detection and resolution, Resolve _only_ provides collision and resolution (including some helpful interfaces that allow you to provide contextual data and callbacks with objects). 

Any form of memory management, optimisations (when to invoke the collider) or anything extra (such as if you need objects to be pushed out) needs to be done by you.

Resolve also provides a helpful primitives class with methods to create polygons of common shapes.

Resolve is made with Monogame in mind and uses the Monogame Vector2 class, however it is very simple to remove all references and use with whatever else.

## Why?

Because I hate writing collision resolution code and could never find adequate resources on dealing with actual collision _resolution_ (dealing with collision _detection_ is easy) or an actual library that wasn't an overcomplicated physics engine that had too many bells and whistles.

I took the time to just put together a one-size-fits-all approach that I can just reuse in future, and I invite you to freely use it too!

## Usage
TODO

By the way, when creating a Polygon from a list of points, you don't need to close it. :)

## Todo
 - uhh actually explain how to use the thing.
 - Remove unused namespaces + references.
 - Add comments for methods + fields.
 - Helper class to easily import Tiled maps

## Thanks
 - [r/Monogame Discord](https://discord.gg/HtdnrfE) for the more obscure resources and helpful information.
 - [Laurent Cozic](https://www.codeproject.com/Articles/15573/D-Polygon-Collision-Detection) for pretty much the hard work.

## License

MIT © Carim A, Glaciate, see LICENSE.md for a more detailed license.
You're free to do whatever, if you have any improvements, please share!