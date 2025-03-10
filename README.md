# HomeownersMS Guide to Git Pulling/Pushing

## How to Pull From Repo For the First Time?

1. Create a new folder
2. Open cmd sa kana nga folder
3. `git clone https://github.com/VulpritProoze/ELNET_HomeownersMS`
4. `cd name_sa_folder`
5. `git fetch origin`
6. `git checkout development`
7. `git pull origin development`


## How to Push?

1. Go to the root directory of your folder (I think ELNET_HomeownersMS ang name)
2. `git add .`
3. `git commit -m "message here"`
4. `git push origin development`

## How to Pull Again After Naay Changes sa Remote Repo
Like nausab ang Development branch (e.g. naay lain ni push ug changes)

1. Go to root directory sa current folder
2. `git checkout development`
3. `git pull origin development`

## How to Push to "frontend"
1. Clone the Repository
If you haven’t cloned the repository yet, run:
```sh
git clone https://github.com/VulpritProoze/ELNET_HomeownersMS.git
cd ELNET_HomeownersMS
```

2. Switch to the `frontend` Branch
```sh
git checkout frontend
```
Or using:
```sh
git switch frontend
```

3. Make Your Changes
Modify the necessary files as required.

4. Stage the Changes
To stage all modified files:
```sh
git add .
```
Or stage specific files:
```sh
git add filename.ext
```

5. Commit the Changes
```sh
git commit -m "Your commit message describing the changes"
```

6. Add remote repository
```
git remote add origin https://github.com/VulpritProoze/ELNET_HomeownersMS.git
```

7. Push the Changes to the Remote `frontend` Branch
```sh
git push origin frontend
```

### Take Note:
 1. If naa kay usbon sa local repo nimo (sa imo computer), you have to always remember to `git pull origin development`. This pulls changes from remote repo (sa Github). This ensures nga smooth imo work inig `git push`.
