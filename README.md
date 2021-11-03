# Graph-isomorphism

This file holds explanation of the simplified version of the algorithm
that differs from the actual implementation.

Graph isomorphism algorithm:

1.Preliminary marking

2.Breaking symmetry

At this point we will have markings of nodes
where every node has unique marking regardless
whether nodes are symmetric.

(3.Sort adjancy matrix according to markers and compare.)

## 1.Preliminary marking:
1.1.Make 3D array 4\*n\*n

1.layer is distances from each node to each node
2.layer is number of neighbors with larger distance than distance for that node
3.layer is number of neighbors with equal distance than distance for that node
4.layer is number of neighbors with smaller distance than distance for that node
(layers might be in different order, but that doesn’t matter)

1.2.Subtitute 4-tuplets in 3D array with single number (\*1)
As result we will get 2D array.

(\*1)Substitution of tuplets
1.Put all tuplets in list
2.Sort list of tuplets
3.Make sure that there are no copies in list of tuplets
4.Use indexes of tuplets as replacement of tuplets

(This is implemented differently, but it archives same result.)

This process must have following qualities:
1.It must be deterministic. (For same set of tuplets we need to get same markers)
2.Each tuplet must have unique marker.
2.1.Same tuplets must have same marker.
2.2.Diferent tuplets must have different markers.

1.3.Sort each row.
Depending on isomorphism, same node could be on different location in adjancy matrix.

1.4.Substitute rows in 2D array with single number (\*1)
As result we will get 1D array.

This 1D array represents preliminary markers.
If graph is symmetric, there will be copies of same marker, and that is a problem,
because if we sort matrix according those markers,
the nodes with same marker could be in different order
and that will give false result that claims that graphs are different when they are not.
For solving this problem is used step "Breaking symmetry"

## 2. Breaking symmetry

2.1. Find marker that repeat itself.

2.2. Take one node with that marker and change it, while keeping old marker on other nodes.

2.3. Propagate the change.

2.3.1. Make tuplets for each node.

2.3.1.1. Take all neighbors of a node.

2.3.1.2. Put their markers in list.

2.3.1.3. Sort list. (because order might vary from case to case)

2.3.2. Substitute tuplets with markers.(\*1) Those markers are new markers for nodes.

2.3.3. Repeat this process until reaches all nodes.
Number of repetitions is equivalent with largest distance, which is at worst case n.

2.2. Check whether has copies of the same marker, if it has repeat 2.1., if it doesn’t job is done.
Number of repetitions is less than number of nodes.
