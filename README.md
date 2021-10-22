## ColorClustering - CORE
Programme permettant de réduire le nombre de couleurs d'une image par clustering des couleurs présente sur celle-ci.

Version graphique : *liens a venir*

# Algorithme de Clustering
## K-means
K-means est une méthode permettant de calculer la valeur **moyenne** (mean) d'un ensemble de points qui est le plus proche de notre point K (*d'où K-means*). À chaque itération nous affinons la valeur en cherchant à nouveau les points les plus proches de notre point K.
Les distances implémentées sont :
  - Distance Euclidienne:
![formule distance euclidienne](https://render.githubusercontent.com/render/math?math=\Euclidian%20Distance%20=%20\sqrt{(x_{a}-x_{b})^{2}%20+(y_{a}-y_{b})^{2}%20}\\)
  - Distance de Manhattan: 
![formule distance Manhattan](https://render.githubusercontent.com/render/math?math=\Manhattan%20Distance%20=%20{\left%20|(x_{a}-x_{b})%20+(y_{a}-y_{b})\right%20|%20}\\)


## DBScan
DBScan est une méthode permettant de calculer la valeur moyenne d'un ensemble de points. Pour créer un ressemble, le critère de fusion est la distance entre deux points. Si elle est inférieure ou égale à la valeur définit le point rejoint le groupe. À la fin, quand tous les points se sont tester les uns et autres, nous pouvons passer à la dernière étape qui est la séparation des ensembles trop petits.

# Exemple de Réduction 
## Image Originale
![Grogu Original](https://github.com/M4ti5/ColorClustering/blob/main/img/grogu.png)

## Image K-means : 8 Couleurs | 5 Itérations Euclidien
![Grogu Euclidien](https://github.com/M4ti5/ColorClustering/blob/main/img/groguKmeansEuclidian.png)

## Image K-means : 8 Couleurs | 5 Itérations Manhattan
![Grogu Manhattan](https://github.com/M4ti5/ColorClustering/blob/main/img/groguKmeansManhattan.png)

## Image DBScan : 3 Taille minimal | 5 Distance maximale
![Grogu DBScan](https://github.com/M4ti5/ColorClustering/blob/main/img/groguDBScan.png)

# Benchmarks (à venir)
