const [dataProfile, setProfile] = useState(null);
const [loadingProfile, setLoadingProfile] = useState(true);
const [errorProfile, setProfileError] = useState(null);
const [calledProfile, setCalledProfile] = useState(true);
const fetchProfile = (id)=>{
  async function fcal (){
    const response = await getProfile(id).then(()=>{
      setProfile(response.data);
      setLoadingProfile(false);
      setCalledProfile(false);
    }).catch((error)=>{
      setProfileError(error);
      setLoadingProfile(false);
      setCalledProfile(false);
    })
  }
  fcal();
}