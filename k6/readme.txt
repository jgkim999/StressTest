$ kubectl delete configmap crocodile-stress-test
=> configmap "crocodile-stress-test" deleted

$ kubectl create configmap crocodile-stress-test --from-file /mnt/f/temp/k6/test.js
=> configmap/crocodile-stress-test created

$ kubectl describe configmap crocodile-stress-test

$ kubectl apply -f /mnt/f/temp/k6/custom-resource.yml
=> k6.k6.io/k6-sample configured

$ kubectl get k6
NAME        STAGE      AGE     TESTRUNID
k6-sample   finished   4m28s

$ kubectl get jobs
NAME                    COMPLETIONS   DURATION   AGE
k6-sample-1             1/1           97s        4m45s
k6-sample-2             1/1           97s        4m45s
k6-sample-3             1/1           97s        4m45s
k6-sample-4             1/1           96s        4m45s
k6-sample-initializer   1/1           4s         4m48s
k6-sample-starter       1/1           4s         4m23s

$ kubectl get pods
NAME                          READY   STATUS      RESTARTS   AGE
k6-sample-1-ckgt9             0/1     Completed   0          5m21s
k6-sample-2-xvzkc             0/1     Completed   0          5m21s
k6-sample-3-njmmr             0/1     Completed   0          5m21s
k6-sample-4-z4vx8             0/1     Completed   0          5m21s
k6-sample-initializer-77n6w   0/1     Completed   0          5m24s
k6-sample-starter-x2jqc       0/1     Completed   0          4m59s

$ kubectl logs k6-sample-1-ckgt9

$ kubectl delete -f /mnt/f/temp/k6/custom-resource.yml

// Grapana Dashboard
2587
https://grafana.com/grafana/dashboards/2587-k6-load-testing-results/
