start "StateCenter" Iveely.CloudComputing.StateCenter.exe
start "Supervisor" Iveely.CloudComputing.Supervisor.exe
start "Worker:8001" Iveely.CloudComputing.Worker.exe 8001
start "Worker:8002" Iveely.CloudComputing.Worker.exe 8002
start "Merger" Iveely.CloudComputing.Merger.exe
start "Cacher" Iveely.CloudComputing.Cacher.exe
