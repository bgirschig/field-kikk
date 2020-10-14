# config
host="field@192.168.0.32"
targetDir="~/Desktop"
detectorPath="../../detector/detector_0.3/dist/field_detector_server/"

# cd to script directory
cd $(dirname "$0")

target="$host:$targetDir"

# prepare the target directory structure
ssh $host -T <<EOF
mkdir -p $targetDir
mkdir -p $targetDir/detector
mkdir -p $targetDir/field_kikk.app
EOF

# find the latest app build
for file in ./builds/*.app; do
  [[ $file -nt $latestApp ]] && latestApp=$file
done

echo "Latest app build: $latestApp"
echo "Deploying to: $target"

# deploy the app
rsync -a $latestApp/ $target/field_kikk.app

# deploy the detector
echo "Deploying the detector"
rsync -a $detectorPath $target/detector

echo "Done"
