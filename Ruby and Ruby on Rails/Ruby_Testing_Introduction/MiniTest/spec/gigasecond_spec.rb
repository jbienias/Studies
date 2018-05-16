require 'gigasecond'

describe '#from' do
  it 'returns 01/1/2043 01:46:40 date' do
    date = Time.utc(2011, 4, 25, 0, 0, 0)
    expect(Gigasecond.from(date)).to eq Time.utc(2043, 1, 1, 1, 46, 40)
  end

  it 'returns 02/10/2046 23:46:40 date' do
    date = Time.utc(2015, 1, 24, 22, 0, 0)
    expect(Gigasecond.from(date)).to eq Time.utc(2046, 10, 2, 23, 46, 40)
  end

  it 'returns 03/10/2046 01:46:39 date' do
    date = Time.utc(2015, 1, 24, 23, 59, 59)
    expect(Gigasecond.from(date)).to eq Time.utc(2046, 10, 3, 1, 46, 39)
  end
end
