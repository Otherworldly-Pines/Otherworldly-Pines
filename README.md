# Otherworldly Pines

## Use this link to setup your ssh keys:
https://csecode.case.edu/help/ssh/README#generating-a-new-ssh-key-pair

## Development Environment
- **Unity version:** 2020.1.4f1
- **Recommended IDE:** JetBrains Rider *(free for Case students)*

### Setting up Unity Smart Merge
Unity has a built-in tool to make merging weird files like scenes easier. 
The instructions for setting that up can be found here: [Unity - Manual: Smart Merge](https://docs.unity3d.com/2020.1/Documentation/Manual/SmartMerge.html).

On Mac, the path to `UnityYAMLMerge` is `/Applications/Unity/Hub/Editor/2020.1.4f1/Unity.app/Contents/Tools/UnityYAMLMerge`

## Branch Setup
We'll be using a **protected master branch**. 
What this means, is no user can commit or push directly to master. 
In order to make a change, you need to...
 1. Create a new branch off master
 2. Make your changes in that branch
 3. Open a [merge request](https://docs.gitlab.com/ee/user/project/merge_requests/getting_started.html) to merge your change into master
 4. Request the review of someone you think can give a meaningful assessment of your changes
 5. Once that person has approved the MR, click **squash and merge** to merge your changes into master

### General git notes
- [Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf) with many helpful commands, for anyone who's unfamiliar
- Make many small commits, rather than a few big ones
- Use meaningful commit messages so it's easy to read through changes
