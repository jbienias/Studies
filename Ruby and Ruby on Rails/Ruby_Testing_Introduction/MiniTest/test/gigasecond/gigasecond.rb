class Gigasecond
  def self.from(time)
    time + 10**9
  end
end


module BookKeeping
  VERSION = 6
end


# irb> t = Time.now
#=> 2011-08-03 22:35:01 -0600

#irb> t2 = t + 10               # 10 Seconds
#=> 2011-08-03 22:35:11 -0600

#irb> t3 = t + 10*60            # 10 minutes
#=> 2011-08-03 22:45:01 -0600

#irb> t4 = t + 10*60*60         # 10 hours
#=> 2011-08-04 08:35:01 -0600
